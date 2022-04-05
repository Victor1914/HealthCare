namespace HealthCare.Exceptions.ImagesExceptions
{
    using System;

    public class IncorrectMainPhotosCountException : Exception
    {
        public string StatusCode => "IncorrectMainPhotosCount";

        public override string Message => "Too many main photos.";
    }
}
