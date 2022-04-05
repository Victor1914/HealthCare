namespace HealthCare.Contracts.Models.MedicalManAccount.Data
{
    using Common;
    using MedicalCenterAccount.Data;

    public class WorkLocation
    {
        public int Id { get;set; }

        public MedicalCenterData MedicalCenter { get;set; }

        public CityData City {get; set; }
    }
}
