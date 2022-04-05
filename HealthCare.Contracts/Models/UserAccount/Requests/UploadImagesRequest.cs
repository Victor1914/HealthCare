namespace HealthCare.Contracts.Models.UserAccount.Requests
{
    using System.Collections.Generic;

    using Data;

    public class UploadImagesRequest
    {
        public List<ImageData> Images { get; set; }
    }
}
