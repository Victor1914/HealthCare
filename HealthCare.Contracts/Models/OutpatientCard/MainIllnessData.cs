namespace HealthCare.Contracts.Models.OutpatientCard
{
    using Data;

    public class MainIllnessData : OutPatientCardIllnessData
    {
        public bool IsMain { get; set; } = true;
    }
}
