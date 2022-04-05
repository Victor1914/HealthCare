namespace HealthCare.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    using Contracts.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.UserAccount.Data;
    using Contracts.Models.UserAccount.Requests;
    using Contracts.Models.UserAccount.Responses;
    using DataLayer;
    using DataLayer.Entities.Patient;
    using DataLayer.Entities.UserAccount;
    using DataLayer.Entities.UserAccount.Contacts;
    using Exceptions;
    using Extensions;
    using Helpers;
    using Interfaces;
    using Utilities.Enums;
    using Utilities.Enums.Common;
    using Utilities.Extensions;
    using Utilities.Helpers;

    public class UserService : IUserService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISessionResolver _sessionResolver;
        private readonly IContactsService _contactsService;
        private readonly IImageService _imageService;
        private readonly IAuthService _authService;
        private readonly ICommunicationService _communicationService;

        private const int SaltByteSize = 64;
        private const int HashByteSize = 256;
        private const int HashingIterationsCount = 10000;

        public UserService(HealthCareDbContext dbContext,
            IMapper mapper,
            ISessionResolver sessionResolver,
            IContactsService contactsService,
            IImageService imageService,
            IAuthService authService,
            ICommunicationService communicationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sessionResolver = sessionResolver;
            _contactsService = contactsService;
            _imageService = imageService;
            _authService = authService;
            _communicationService = communicationService;
        }

        public async Task RegisterUser(RegisterUserRequest request)
        {
            var user = new User
            {
                RoleType = request.UserRole
            };

            if (request.UserRole == RoleType.Doctor)
                await SetDoctorData(request.GeneralData, user);
            
            var userContact = new UserContact
            {
                User = user
            };

            await _dbContext.AddRangeAsync(user, userContact);

            if (request.UserRole == RoleType.Patient)
            {
                var infoModel = (PatientInfo)_dbContext.AddInfoModel(user.Id, request.Name, user.RoleType, request.IdentityNumber.GetGender());
                infoModel.EGN = long.Parse(request.IdentityNumber);
                infoModel.BirthDate = request.IdentityNumber.GetBirthDate();
            }
            else
            {
                _dbContext.AddInfoModel(user.Id, request.Name, user.RoleType);
            }
            
            PersistUserContactsOnRegistration(request.Contacts, userContact.Id, DatabaseOperation.Insert);

            await _dbContext.SaveChangesAsync();

            var (emailMessage, model) = EmailBuilderHelper.BuildWelcomeEmail(request.GeneralData.Username ?? request.Name, request.Contacts.Email.EmailAddress);
            await _communicationService.SendEmail(emailMessage, model);
        }

        public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == request.Username);
            ValidationUtils.ValidateAndThrow<IncorrectUserDataException>(() => user == null);

            var isPasswordMatch = VerifyPassword(request.Password, user.PasswordHash, user.Secret);
            ValidationUtils.ValidateAndThrow<IncorrectUserDataException>(() => !isPasswordMatch);

            var token = _authService.GenerateToken(user.Username, user.Id);

            return new LoginUserResponse
            {
                Username = request.Username,
                Token = token
            };
        }

        public async Task<TokenData> AddContact(AddContactRequest request)
        {
            var userContactId = _dbContext.UserContacts.SingleOrDefault(x => x.UserId == _sessionResolver.SessionInfo.UserId)?.Id;

            PersistUserContacts(request.Contacts, userContactId.GetValueOrDefault());

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public async Task<EditUserGeneraDataResponse> EditGeneralData(EditUserGeneraDataRequest request)
        {
            var user = RetrieveUser(_sessionResolver.SessionInfo.Username, _sessionResolver.SessionInfo.UserId);

            if (!request.GeneralData.IsNullOrEmpty())
            {
                EditUsernameAndPass(request.GeneralData, user);
            }

            var userContactId = _dbContext.UserContacts.SingleOrDefault(x => x.UserId == _sessionResolver.SessionInfo.UserId)?.Id;
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => userContactId == 0);

            PersistUserContacts(request.Contacts, userContactId.GetValueOrDefault());

            await _dbContext.SaveChangesAsync();

            return new EditUserGeneraDataResponse
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public async Task<TokenData> UploadImages(UploadImagesRequest request)
        {
            UploadProfilePhotos(request.Images);

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            var emailContact = _dbContext.Emails.SingleOrDefault(email => email.EmailAddress.Contains(request.Email));
            ValidationUtils.ValidateAndThrow<NonExistingEmailException>(() => emailContact == null);

            var user = _dbContext.Users.SingleOrDefault(usr => usr.UserContact.Id == emailContact.UserContactId);

            var newPassword = Guid.NewGuid().ToString();
            var (hashedPassword, secret) = GenerateSaltedHash(newPassword);

            user.PasswordHash = hashedPassword;
            user.Secret = secret;

            await _dbContext.SaveChangesAsync();

            var (emailMessage, model) = EmailBuilderHelper.BuildForgottenPasswordEmail(user.Username, request.Email, newPassword);

            await _communicationService.SendEmail(emailMessage, model);

        }

        public async Task<TokenData> ChangePassword(ChangePasswordRequest request)
        {
            var user = RetrieveUser(_sessionResolver.SessionInfo.Username, _sessionResolver.SessionInfo.UserId);

            var isPasswordMatch = VerifyPassword(request.OldPassword, user.PasswordHash, user.Secret);
            ValidationUtils.ValidateAndThrow<IncorrectPasswordException>(() => !isPasswordMatch);

            var (hashedPassword, secret) = GenerateSaltedHash(request.NewPassword);
            user.PasswordHash = hashedPassword;
            user.Secret = secret;

            await _dbContext.SaveChangesAsync();

            var token = _authService.GenerateToken(user.Username, user.Id);

            return new TokenData
            {
                Token = token
            };
        }

        public void UploadProfilePhotos(List<ImageData> imagesInRequest)
        {
            var userPhotos = _dbContext.Photos
                .Where(x => x.UserId == _sessionResolver.SessionInfo.UserId)
                .Select(x => x.Id)
                .ToList();

            var existedPhotosInRequest = imagesInRequest
                .Where(x => x.Id != 0)
                .Select(x => x.Id)
                .ToList();

            ValidationUtils.ValidateAndThrow<DataMismatchException>(
                () => existedPhotosInRequest.Count > 0 && !existedPhotosInRequest.All(userPhotos.Contains));

            _imageService.UploadImages(imagesInRequest);
        }

        public async Task AddMedicalCenter(AddMedicalCenterRequest request)
        {
            var user = new User
            {
                Username = request.Name,
                RoleType = RoleType.MedicalCenter
            };

            var userContact = new UserContact
            {
                User = user
            };

            await _dbContext.AddRangeAsync(user, userContact);

            _dbContext.AddInfoModel(user.Id, request.Name, user.RoleType);
            _contactsService.PersistEntity<AddressData, Address>(request.Address, userContact.UserId, DatabaseOperation.Insert);

            await _dbContext.SaveChangesAsync();
        }

        private User RetrieveUser(string username, int userId)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == userId && x.Username == username);
            ValidationUtils.ValidateAndThrow<IncorrectUserDataException>(() => user == null);

            return user;
        }

        private static void EditUsernameAndPass(GeneralUserData data, User user)
        {
            if (data.Username != null)
            {
                user.Username = data.Username;
            }

            if (data.Password != null)
            {
                var (hashedPassword, salt) = GenerateSaltedHash(data.Password);
                user.PasswordHash = hashedPassword;
                user.Secret = salt;
            }

            user.UpdateDate = DateTime.Now;
        }

        private void PersistUserContactsOnRegistration(RegisterContactData contacts, int userContactId, DatabaseOperation operation)
        {
            _contactsService.PersistEntity<EmailData, Email>(contacts.Email, userContactId, operation);
            _contactsService.PersistEntity<PhoneData, Phone>(contacts.Phone, userContactId, operation);
            _contactsService.PersistEntity<AddressData, Address>(contacts.Address, userContactId, operation);
        }

        private void PersistUserContacts(ContactUserData contacts, int userContactId)
        {
            var emails = contacts.Emails.EmptyIfNull();
            foreach (var email in emails)
                _contactsService.PersistEntity<EmailData, Email>(email, userContactId, email.Id.GetDbOperation());

            var phones = contacts.Phones.EmptyIfNull();
            foreach (var phone in phones)
                _contactsService.PersistEntity<PhoneData, Phone>(phone, userContactId, phone.Id.GetDbOperation());
        }

        private async Task SetDoctorData(GeneralUserData generalData, User user)
        {
            var userExists = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == generalData.Username);
            ValidationUtils.ValidateAndThrow<ExistedUserNameException>(() => userExists != null);

            var (hashedPassword, secret) = GenerateSaltedHash(generalData.Password);

            user.Username = generalData.Username;
            user.PasswordHash = hashedPassword;
            user.Secret = secret;
        }

        private static (string, string) GenerateSaltedHash(string password)
        {
            using var provider = new RNGCryptoServiceProvider();
            var saltBytes = new byte[SaltByteSize];
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            using var hashGenerator = new Rfc2898DeriveBytes(password, saltBytes, HashingIterationsCount);
            var hashPassword = Convert.ToBase64String(hashGenerator.GetBytes(HashByteSize));

            return (hashPassword, salt);
        }

        private static bool VerifyPassword(string password, string storedHash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using var hashGenerator = new Rfc2898DeriveBytes(password, saltBytes, HashingIterationsCount);
            var generatedHash = Convert.ToBase64String(hashGenerator.GetBytes(HashByteSize));

            return generatedHash == storedHash;
        }
    }
}
