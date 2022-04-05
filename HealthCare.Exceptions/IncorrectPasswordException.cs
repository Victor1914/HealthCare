namespace HealthCare.Exceptions
{
    using System;

    public class IncorrectPasswordException : Exception
    {
        public string StatusCode => "PasswordMismatch";

        public override string Message => "Password is wrong.";
    }
}