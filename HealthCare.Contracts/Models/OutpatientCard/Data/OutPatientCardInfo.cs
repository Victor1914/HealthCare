namespace HealthCare.Contracts.Models.OutpatientCard.Data
{
    public class OutPatientCardInfo : OutpatientCardBaseInfo
    {
        public int Id {get; set; }

        public string DoctorName { get; set; }

        public string DoctorSpecialty { get; set; }

        public string UIN { get; set; }
    }
}

