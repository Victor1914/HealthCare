namespace HealthCare.Exceptions
{
    using System;

    public class UnauthorizedUserException : Exception
    {
        public string StatusCode => "UnauthorizedUser";

        public override string Message => "User is not authorized for the action";
    }

}
