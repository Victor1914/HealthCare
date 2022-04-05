namespace HealthCare.Contracts.Models.MedicalCenterAccount.Data
{
    using HealthCare.Interfaces;

    public class MedicalCenterData : IName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
