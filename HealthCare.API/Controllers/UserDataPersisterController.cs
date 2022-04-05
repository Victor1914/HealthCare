namespace HealthCare.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Behaviour.Filters;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.UserAccount.Requests;
    using Contracts.Models.UserAccount.Responses;
    using Utilities.Enums;

    [ApiController]
    [ValidationFilter]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Patient, RoleType.Doctor, RoleType.MedicalCenter, RoleType.Pharmacy, RoleType.PharmacyCompany)]
    [Route("user")]
    public class UserDataPersisterController : ControllerBase
    {
        private readonly IUserService _userService;
        

        public UserDataPersisterController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("register")]
        [HttpPost]
        [DisableCustomAuthorizationFilter]
        public async Task Register(RegisterUserRequest request)
        { 
            await _userService.RegisterUser(request);
        }

        [Route("login")]
        [HttpPost]
        [DisableCustomAuthorizationFilter]
        public async Task<LoginUserResponse> Login(LoginUserRequest request)
        {
            return await _userService.LoginUser(request);
        }

        [Route("forgotPassword")]
        [HttpPost]
        [DisableCustomAuthorizationFilter]
        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            await _userService.ForgotPassword(request);
        }

        [Route("changePassword")]
        [HttpPost]
        public async Task ChangePassword(ChangePasswordRequest request)
        {
            await _userService.ChangePassword(request);
        }

        [Route("editData")]
        [HttpPost]
        public async Task<EditUserGeneraDataResponse> EditGeneralInfo(EditUserGeneraDataRequest request)
        {
            return await _userService.EditGeneralData(request);
        }

        [Route("editContacts")]
        [HttpPost]
        public async Task<TokenData> AddContact(AddContactRequest request)
        {
            return await _userService.AddContact(request);
        }

        [Route("uploadImages")]
        [HttpPost]
        public async Task<TokenData> UploadImages(UploadImagesRequest request)
        {
            return await _userService.UploadImages(request);
        }

        [Route("addMedicalCenter")]
        [HttpPost]
        [DisableCustomAuthorizationFilter]
        public async Task<OkObjectResult> AddMedicalCenter(AddMedicalCenterRequest request)
        {
            await _userService.AddMedicalCenter(request);

            return Ok("Bingo");
        }
    }
}