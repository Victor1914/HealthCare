namespace HealthCare.Utilities.Extensions
{
    using System.Collections.Generic;
    
    using Enums;

    public static class CommonExtensions
    {
        public static bool Between(this int number, int minValue, int maxValue, bool inclusive = true)
        {
            return inclusive
                ? minValue <= number && number <= maxValue
                : minValue < number && number < maxValue;
        }

        public static void AddRange<T>(this ICollection<T> collection, params T[] itemsToAdd)
        {
            foreach (var item in itemsToAdd)
                collection.Add(item);
        }

        public static List<AppraisalType> GetValidAppraisalTypes(this AppraisalRecipientType recipientType)
        {
            var validAppraisalTypes = new List<AppraisalType>
            {
                AppraisalType.Attitude,
                AppraisalType.Conditions,
                AppraisalType.CommonValuation
            };

            switch (recipientType)
            {
                case AppraisalRecipientType.Doctor:
                    validAppraisalTypes.AddRange(AppraisalType.WaitingTime, AppraisalType.TreatmentEffectiveness);
                    break;
                case AppraisalRecipientType.MedicalCenter:
                    validAppraisalTypes.Add(AppraisalType.Hygiene);
                    break;
            }

            return validAppraisalTypes;
        }
    }
}
