namespace HealthCare.BusinessLayer.Extensions
{
    using System;
    using Utilities.Enums;

    public static class IdentityNumberExtensions
    {
        public static Gender GetGender(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return Gender.None;

            return id[^1] % 2 != 0 ? Gender.Female : Gender.Male;
        }

        public static DateTime GetBirthDate(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return DateTime.MinValue;

            const int century21ThIdentifier = 40;
            const int century21ThValue = 2000;
            const int previousCenturyValue = 1900;

            int.TryParse(id.Substring(0, 2), out  var year);
            int.TryParse(id.Substring(2, 2), out  var month);
            int.TryParse(id.Substring(4, 2), out  var day);

            if (month > century21ThIdentifier)
            {
                month -= century21ThValue;
                year = century21ThValue + year;
            }
            else
            {
                year = previousCenturyValue + year;
            }

            return new DateTime(year, month, day);
        }
    }
}