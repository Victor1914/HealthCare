namespace HealthCare.Exceptions
{
    using System;

    public class NonExistingEmailException : Exception
    {
        public string StatusCode => "DataMismatch";

        public override string Message => "Email does not exist in the system";
    }
}
