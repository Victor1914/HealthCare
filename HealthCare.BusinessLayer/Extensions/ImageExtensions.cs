namespace HealthCare.BusinessLayer.Extensions
{
    using System;
    using System.IO;
    using Exceptions.ImagesExceptions;
    using Microsoft.AspNetCore.Http;
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public static class ImageExtensions
    {
        public static string Upload(this FileInfo image, string destinationPath)
        {
            image.ValidateImage();

            var imageName = Guid.NewGuid().ToString();
            image.CopyTo(Path.Combine(destinationPath, imageName));

            return imageName;
        }

        public static void ValidateImage(this IFormFile image)
        {
            var extenstion = Path.GetExtension(image.FileName);

            ValidationUtils.ValidateAndThrow<InvalidImageFormatException>(
                () => !Enum.IsDefined(typeof(ImageFormat), extenstion));

            ValidationUtils.ValidateAndThrow<InvalidImageSizeException>(() => image.Length > 2100000);
        }

        private static void ValidateImage(this FileInfo image)
        {
            ValidationUtils.ValidateAndThrow<InvalidImageFormatException>(
                () => !Enum.IsDefined(typeof(ImageFormat), image.Extension.Replace(".", "")));

            ValidationUtils.ValidateAndThrow<InvalidImageSizeException>(() => image.Length > 2100000);
        }
    }
}
