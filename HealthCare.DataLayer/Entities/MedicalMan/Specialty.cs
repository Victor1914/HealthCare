namespace HealthCare.DataLayer.Entities.MedicalMan
{
    using System.Collections.Generic;
    
    using Base;

    public class Specialty : SystemData
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<MedicalManSpecialty> MedicalMan { get; set; } = new List<MedicalManSpecialty>();
    }
}
