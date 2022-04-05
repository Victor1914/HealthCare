namespace HealthCare.Contracts.Models.Common
{
    using Newtonsoft.Json;

    public class CommonMedicalData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string MkbCode { get; set; }
    }
}
