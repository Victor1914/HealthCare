namespace HealthCare.DataLayer.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    using Contracts.Configuration;
    using HealthCare.Interfaces;

    public class DataRetriever : IDataRetriever
    {
        private readonly IOptionsSnapshot<SeedSettings> _seedSettings;
        
        public DataRetriever(IOptionsSnapshot<SeedSettings> seedSettings)
        {
            _seedSettings = seedSettings;
        }

        public List<TEntity> RetrieveData<TEntity>() where TEntity : class
        {
            var localPath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            var fullPath = Path.Combine(localPath, _seedSettings.Value.SeedDirectory, $"{typeof(TEntity).Name}.json");

            var content = File.ReadAllText(fullPath);

            return JsonConvert.DeserializeObject<List<TEntity>>(content);
        }
    }
}
