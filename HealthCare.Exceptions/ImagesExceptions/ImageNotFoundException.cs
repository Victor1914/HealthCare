namespace HealthCare.Exceptions.ImagesExceptions
{
    using System;

    public class ImageNotFoundException : Exception
    {
        public string StatusCode => "ImageNotFound";

        public override string Message => "Image is not found";
    }
}
