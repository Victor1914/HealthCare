namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;

    using DataLayer.Entities.Base;
    using HealthCare.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Utilities.Enums;

    public interface IMedicalDataService
    {
        void PersistMedicalDataRelatedEntities<TMedicalEntity, TModel>(IEnumerable<TMedicalEntity> medicalEntities,
            int documentId, DocumentType documentType)
            where TModel : SystemData, IMedicalData;

        public void PersistMedicalDataRelatedEntity<TMedicalEntity, TModel>(TMedicalEntity medicalEntity,
            int documentId, DocumentType documentType)
            where TModel : SystemData, IMedicalData;

        void PersistMedicalTests(IList<IFormFile> files, int documentId, DocumentType documentType);
    }
}
