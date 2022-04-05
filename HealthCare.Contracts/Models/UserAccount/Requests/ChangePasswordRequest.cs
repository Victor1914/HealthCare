namespace HealthCare.Contracts.Models.UserAccount.Requests
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
