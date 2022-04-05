namespace HealthCare.Exceptions.ImagesExceptions
{
    using System;

    public class InvalidImageFormatException : Exception
    {
        public string StatusCode => "InvalidImageFormat";

        public override string Message => "Image format is not supported";
    }
}
