namespace HealthCare.Contracts.Models.UserAccount.Requests
{
    using Data;
    using HealthCare.Interfaces;
    using Utilities.Enums;

    public class RegisterUserRequest : IName
    {
        public GeneralUserData GeneralData { get; set; }

        public string Name { get; set; }

        public RoleType UserRole { get; set; }

        public string IdentityNumber { get; set; }

        public int NotificationType { get; set; }

        public RegisterContactData Contacts { get; set; }
    }
}
