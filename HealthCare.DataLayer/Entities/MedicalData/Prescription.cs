namespace HealthCare.DataLayer.Entities.MedicalData 
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Base;
    using UserAccount;

    public class Prescription : SystemData
    {
        public string MedicamentName { get; set; }

        public int DaysCount { get; set; }

        public int Quantity { get; set; }

        public bool TakeMorning { get; set; }

        public bool TakeNight { get; set; }

        public bool BeforeMeal { get; set; }

        public bool AfterMeal { get; set; }

        public bool OnTwelveHours { get; set; }

        public string Notes { get; set; }

        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public User Doctor { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public User Patient { get; set; }
    }
}
