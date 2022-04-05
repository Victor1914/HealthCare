namespace HealthCare.Exceptions.JWTExceptions
{
    using System;

    public class ExpiredTokenException : Exception
    {
        public string StatusCode => "ExpiredToken";

        public override string Message => "Given token is expired.";
    }
}
