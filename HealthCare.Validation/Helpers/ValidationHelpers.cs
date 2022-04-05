namespace HealthCare.Validation.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    
    using Contracts.Models.UserAccount.Data;

    public static class ValidationHelpers
    {
        public static bool HasValidEmails(this List<EmailData> emails)
        {
            var regexValidator = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

            return emails.All(email => regexValidator.IsMatch(email.EmailAddress));
        }

        public static bool HasValidPhoneNumbers(this List<PhoneData> phonesNumbers)
        {
            var regexValidator = new Regex(@"^(\+?[0-9]{8,20})$");

            return phonesNumbers.All(phone => regexValidator.IsMatch(phone.Number));
        }
    }
}
