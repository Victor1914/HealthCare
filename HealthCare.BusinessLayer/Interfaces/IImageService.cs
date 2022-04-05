namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;

    using Contracts.Models.UserAccount.Data;
    using Utilities.Enums;
    using Utilities.Enums.Common;

    public interface IImageService
    {
        void UploadImage(ImageData img, DatabaseOperation operation);

        void UploadImages(List<ImageData> images);
    }
}
