namespace HealthCare.Utilities.Extensions
{
    using System.Linq;

    public static class ReflectionExtensions
    {
        public static bool IsNullOrEmpty(this object value)
        {
            return value.GetType().GetProperties().All(p => p.GetValue(value) == null);
        }
    }
}
