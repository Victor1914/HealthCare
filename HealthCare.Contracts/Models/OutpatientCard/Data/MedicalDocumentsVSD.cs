namespace HealthCare.Contracts.Models.OutpatientCard.Data
{
    public class MedicalDocumentsVSD : MedicalDocuments
    {
        public bool IsVSD { get; set; } = true;

        public int VSDCode { get; set; }
    }
}
