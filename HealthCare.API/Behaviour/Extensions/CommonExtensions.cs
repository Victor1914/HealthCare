namespace HealthCare.API.Behaviour.Extensions
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Filters;

    public static class CommonExtensions
    {
        public static bool IsAttrDisabled<TAttributeType>(this FilterContext context)
        {
            return context.Filters.Any(f => f.GetType() == typeof(TAttributeType));
        }
    }
}