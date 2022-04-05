namespace HealthCare.BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.IO;
    using AutoMapper;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;

    using Contracts.Configuration;
    using DataLayer;
    using DataLayer.Entities.Base;
    using DataLayer.Entities.MedicalData;
    using Extensions;
    using HealthCare.Interfaces;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using Utilities.Enums;

    public class MedicalDataService : IMedicalDataService
    {
        private readonly IMapper _mapper;
        private readonly HealthCareDbContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IOptionsSnapshot<CommonSettings> _settings;

        public MedicalDataService(IMapper mapper,
            HealthCareDbContext dbContext,
            IHostingEnvironment appEnvironment,
            IOptionsSnapshot<CommonSettings> settings)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _appEnvironment = appEnvironment;
            _settings = settings;
        }

        public void PersistMedicalDataRelatedEntities<TMedicalEntity, TModel>(IEnumerable<TMedicalEntity> medicalEntities, int documentId, DocumentType documentType)
            where TModel : SystemData, IMedicalData
        {
            foreach (var entity in medicalEntities)
            {
                PersistMedicalDataRelatedEntity<TMedicalEntity, TModel>(entity, documentId, documentType);
            }
        }

        public void PersistMedicalDataRelatedEntity<TMedicalEntity, TModel>(TMedicalEntity medicalEntity, int documentId, DocumentType documentType)
            where TModel : SystemData, IMedicalData
        {
            if(medicalEntity == null)
                return;

            var dbModel = _mapper.Map<TModel>(medicalEntity);

            dbModel.SetDocumentId(documentType, documentId);

            var dbOperation = dbModel.Id.GetDbOperation();
            _dbContext.PersistModel(dbModel, dbOperation);
        }

        public void PersistMedicalTests(IList<IFormFile> files, int documentId, DocumentType documentType)
        {
            foreach (var file in files)
            {
                file.ValidateImage();

                var medTestDbModel = new MedicalTest();

                medTestDbModel.SetDocumentId(documentType, documentId);

                var medicalTestId = _dbContext.PersistModel(medTestDbModel, medTestDbModel.Id.GetDbOperation());

                var attachmentDbModel = new MedicalTestAttachment();

                var url = Path.Combine(_appEnvironment.WebRootPath, _settings.Value.MedicalTestAttachmentPath, file.FileName);

                using (Stream stream = new FileStream(url, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                attachmentDbModel.Url = Path.Combine(url, file.FileName);
                attachmentDbModel.MedicalTestId = medicalTestId;

                _dbContext.PersistModel(attachmentDbModel, attachmentDbModel.Id.GetDbOperation());
            }
        }
    }
}
