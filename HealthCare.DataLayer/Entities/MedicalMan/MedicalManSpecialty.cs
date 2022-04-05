namespace HealthCare.DataLayer.Entities.MedicalMan
{
    using Base;

    public class MedicalManSpecialty : SystemData
    {
        public int MedManInfoId { get; set; }
        public MedicalManInfo MedManInfo { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
