namespace HealthCare.Contracts.Models.MedicalManAccount.Requests
{
    using System.Collections.Generic;

    using Data;

    public class PersistPersonalDataRequest
    {
        public MedicalManGeneralData GeneralDataData { get; set; }

        public List<int> Specialties { get; set; }

        public List<int> MedicalCenters { get; set; }
    }
}
