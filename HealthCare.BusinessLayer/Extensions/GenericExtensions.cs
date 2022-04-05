namespace HealthCare.BusinessLayer.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    using DataLayer.Entities.Base;
    using DataLayer.Interfaces;
    using HealthCare.Interfaces;
    using Utilities.Enums;
    using Utilities.Enums.Common;

    public static class GenericExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable) => enumerable ?? Enumerable.Empty<T>();

        public static List<T> EmptyIfNullToList<T>(this IEnumerable<T> enumerable) => enumerable.ToList();

        public static int PersistModel<TModel>(this DbContext dbContext, TModel model, DatabaseOperation operation)
            where TModel : SystemData
        {
            switch (operation)
            {
                case DatabaseOperation.Update:
                    {
                        model.UpdateDate = DateTime.UtcNow;

                        dbContext.Entry(model).State = EntityState.Modified;
                        break;
                    }

                case DatabaseOperation.Insert:
                    {
                        dbContext.Entry(model).State = EntityState.Added;
                        break;
                    }
            }

            return model.Id;
        }

        public static void SetDocumentId<TModel>(this TModel model, DocumentType documentType, int documentId)
            where TModel : IMedicalData
        {
            switch (documentType)
            {
                case DocumentType.MedicalProfile:
                    model.MedicalProfileId = documentId;
                    break;
                case DocumentType.OutpatientCard:
                    model.OutpatientCardId = documentId;
                    break;
            }
        }

        public static void DeleteEntity<TModel>(this DbContext dbContext, TModel model)
            where TModel : SystemData
        {
            model.UpdateDate = DateTime.UtcNow;
            model.IsDeleted = true;

            dbContext.Entry(model).State = EntityState.Modified;
        }

        public static TModel CreateInfoObject<TModel>(this DbContext dbContext, string name, int userId, Gender gender = Gender.None)
            where TModel : IInformation, new()
        {
            var infoModel = new TModel
            {
                Name = name,
                UserId = userId,
                Gender = gender
            };

            dbContext.Add(infoModel);

            return infoModel;
        }

        public static DatabaseOperation GetDbOperation(this int modelId)
        {
            return modelId == 0 ? DatabaseOperation.Insert : DatabaseOperation.Update;
        }

        public static TModel SingleOrNew<TModel>(this IQueryable<TModel> source, Expression<Func<TModel, bool>> predicate)
            where TModel : class, new()
        {
            return source.SingleOrDefault(predicate) ?? new TModel();
        }

        public static object NullIfDefault<T>(this T value)
        {
            return value == null || EqualityComparer<T>.Default.Equals(value, default) ? (object) null : value;
        }
    }
}
