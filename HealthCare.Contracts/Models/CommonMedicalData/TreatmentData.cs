namespace HealthCare.Contracts.Models.CommonMedicalData
{
    using System.Collections.Generic;
    using Utilities.Enums;

    public class TreatmentData 
    {
        public int Id { get; set; }

        public string CommonTreatmentNotes { get; set; }

        public TreatmentResult TreatmentResult { get; set; }

        public List<MedicamentData> Medicaments { get; set; }
    }
}
