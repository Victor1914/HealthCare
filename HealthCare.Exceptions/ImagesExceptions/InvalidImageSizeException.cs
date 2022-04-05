namespace HealthCare.Exceptions.ImagesExceptions
{
    using System;

    public class InvalidImageSizeException : Exception
    {
        public string StatusCode => "InvalidImageSize";

        public override string Message => "Image Size is not supported";
    }
}
