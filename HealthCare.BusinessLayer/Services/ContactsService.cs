namespace HealthCare.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using DataLayer;
    using Exceptions;
    using HealthCare.Interfaces;
    using Interfaces;
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public class ContactsService : IContactsService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly IMapper _mapper;

        public ContactsService(HealthCareDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void PersistEntities<TRequestObj, TDBObject>(IEnumerable<TRequestObj> requestEntities, int userContactId, DatabaseOperation operation)
            where TDBObject : class, IContactIdentity, new()
            where TRequestObj : class
        {
            foreach (var requestEntity in requestEntities)
            {
                PersistEntity<TRequestObj, TDBObject>(requestEntity, userContactId, operation);
            }
        }

        public void PersistEntity<TRequestObj, TDBObject>(TRequestObj requestEntity, int userContactId, DatabaseOperation operation)
            where TDBObject : class, IContactIdentity, new()
            where TRequestObj : class
        {
            if(requestEntity == null)
                return;

            var map = _mapper.Map<TDBObject>(requestEntity);
            map.UserContactId = userContactId;

            switch (operation)
            {
                case DatabaseOperation.Insert:
                    {
                        _dbContext.Entry(map).State = EntityState.Added;
                        break;
                    }

                case DatabaseOperation.Update:
                    {
                        var userContactExist = _dbContext.Set<TDBObject>()
                            .Any(x => x.Id == map.Id && x.UserContactId == userContactId);
                        ValidationUtils.ValidateAndThrow<DataMismatchException>(() => !userContactExist);

                        map.UpdateDate = DateTime.Now;

                        _dbContext.Entry(map).State = EntityState.Modified;
                        break;
                    }
            }
        }
    }
}
