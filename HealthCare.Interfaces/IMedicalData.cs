namespace HealthCare.Interfaces
{
    public interface IMedicalData
    {
        int Id { get; set; }

        int? MedicalProfileId { get; set; }

        int? OutpatientCardId { get; set; }
    }
}
