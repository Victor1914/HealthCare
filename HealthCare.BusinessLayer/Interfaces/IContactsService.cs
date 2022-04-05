namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;

    using HealthCare.Interfaces;
    using Utilities.Enums.Common;

    public interface IContactsService
    {
        void PersistEntity<TRequestObj, TDBObject>(TRequestObj requestEntity, int userContactId, DatabaseOperation operation)
            where TDBObject : class, IContactIdentity, new()
            where TRequestObj : class;

        void PersistEntities<TRequestObj, TDBObject>(IEnumerable<TRequestObj> requestEntities, int userContactId, DatabaseOperation operation)
            where TDBObject : class, IContactIdentity, new()
            where TRequestObj : class;

    }
}