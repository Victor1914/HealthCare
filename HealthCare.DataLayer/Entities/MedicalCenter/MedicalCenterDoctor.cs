namespace HealthCare.DataLayer.Entities.MedicalCenter
{
    using Base;
    using UserAccount;

    public class MedicalCenterDoctor : SystemData
    {
        public int DoctorId { get; set; }
        public User Doctor { get; set; }

        public int MedicalCenterId { get; set; }
        public User MedicalCenter { get; set; }
    }
}
