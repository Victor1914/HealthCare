namespace HealthCare.Exceptions
{
    using System;

    public class ExistedUserNameException : Exception
    {
        public string StatusCode => "UserNameExists";

        public override string Message => "Username has been already exist.";
    }
}
