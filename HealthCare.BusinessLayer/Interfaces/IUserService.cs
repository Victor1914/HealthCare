namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;
    
    using Contracts.Models.Common;
    using Contracts.Models.UserAccount.Requests;
    using Contracts.Models.UserAccount.Responses;

    public interface IUserService
    {
        Task RegisterUser(RegisterUserRequest request);

        Task<LoginUserResponse> LoginUser(LoginUserRequest request);

        Task<TokenData> AddContact(AddContactRequest request);

        Task<EditUserGeneraDataResponse> EditGeneralData(EditUserGeneraDataRequest request);

        Task<TokenData> UploadImages(UploadImagesRequest request);

        Task ForgotPassword(ForgotPasswordRequest request);

        Task<TokenData> ChangePassword(ChangePasswordRequest request);

        Task AddMedicalCenter(AddMedicalCenterRequest request);
    }
}