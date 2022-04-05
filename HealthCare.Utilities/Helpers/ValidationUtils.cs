namespace HealthCare.Utilities.Helpers
{
    using System;

    public static class ValidationUtils
    {
        public static void ValidateAndThrow<TException>(Func<bool> condition) 
            where TException : Exception, new()
        {
            if (condition.Invoke())
                throw new TException();
        }
    }
}
