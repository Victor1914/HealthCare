namespace HealthCare.Exceptions
{
    using System;

    public class DataMismatchException : Exception
    {
        public string StatusCode => "DataMismatch";

        public override string Message => "Incorrect data in request";
    }
}
