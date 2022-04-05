namespace HealthCare.Contracts.Models.OutpatientCard.Data
{
    public class OutPatientCardIllnessData
    {
        public int Id { get; set; }

        public int IllnessTypeId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get;set; }
    }
}
