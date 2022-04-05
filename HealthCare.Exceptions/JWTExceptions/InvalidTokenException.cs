namespace HealthCare.Exceptions.JWTExceptions
{
    using System;

    public class InvalidTokenException : Exception
    {
        public string StatusCode => "InvalidToken";

        public override string Message => "Given token is invalid";
    }
}
