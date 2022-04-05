namespace HealthCare.Exceptions
{
    using System;

    public class OperationNotAllowedException : Exception
    {
        public string StatusCode => "OperationNotAllowed";

        public override string Message => "Operation is not allowed.";
    }
}
