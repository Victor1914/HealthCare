namespace HealthCare.Interfaces
{
    using System.Collections.Generic;

    public interface IDataRetriever
    {
        List<TEntity> RetrieveData<TEntity>() where TEntity : class;
    }
}
