namespace HealthCare.Contracts.Models.Prescription.Data
{
    public class PrescriptionData
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string MedicamentName { get; set; }

        public int DaysCount { get; set; }

        public int Quantity { get; set; }

        public bool TakeMorning { get; set; }

        public bool TakeNight { get; set; }

        public bool BeforeMeal { get; set; }

        public bool AfterMeal { get; set; }

        public bool OnTwelveHours { get; set; }

        public string Notes { get; set; }
    }
}
