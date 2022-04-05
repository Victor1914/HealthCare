namespace HealthCare.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    
    using Contracts.Configuration;
    using Contracts.Interfaces;
    using Contracts.Models.UserAccount.Data;
    using DataLayer;
    using DataLayer.Entities.UserAccount;
    using Extensions;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Utilities.Enums;
    using Utilities.Enums.Common;

    public class ImageService : IImageService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ISessionResolver _sessionResolver;
        private readonly IOptionsSnapshot<CommonSettings> _settings;

        public ImageService(HealthCareDbContext dbContext,
            IHostingEnvironment appEnvironment,
            ISessionResolver sessionResolver,
            IOptionsSnapshot<CommonSettings> settings)
        {
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
            _sessionResolver = sessionResolver;
            _settings = settings;
        }

        public void UploadImages(List<ImageData> images)
        {
            foreach (var img in images)
            {
                var dbOperation = img.Id.GetDbOperation();

                UploadImage(img, dbOperation);
            }
        }

        public void UploadImage(ImageData img, DatabaseOperation operation)
        {
            switch (operation)
            {
                case DatabaseOperation.Insert:
                {
                    var image = new FileInfo(img.LocalPath);
                    var url = Path.Combine(_appEnvironment.WebRootPath, _settings.Value.ProfilePhotosPath);

                    var imageName = image.Upload(Path.Combine(Directory.GetCurrentDirectory(), url));

                    var dbModel = new Photo
                    {
                        Url = Path.Combine(url, imageName),
                        UserId = _sessionResolver.SessionInfo.UserId,
                        IsMain = img.IsMain
                    };

                    _dbContext.Entry(dbModel).State = EntityState.Added;

                    break;
                }

                case DatabaseOperation.Update:
                {
                    var dbModel = _dbContext.Photos.SingleOrDefault(x => x.Id == img.Id);

                    dbModel.IsMain = img.IsMain;
                    dbModel.UpdateDate = DateTime.Now;

                    _dbContext.Entry(dbModel).State = EntityState.Added;

                    break;
                }
            }
        }
    }
}