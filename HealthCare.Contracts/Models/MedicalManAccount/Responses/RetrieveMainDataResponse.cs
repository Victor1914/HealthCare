namespace HealthCare.Contracts.Models.MedicalManAccount.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;

    public class RetrieveMainDataResponse : TokenData
    {
        public string Username {get;set;}

        public MedicalManGeneralData GeneralData {get;set;}

        public List<WorkLocation> WorkLocations { get; set; } = new List<WorkLocation>();

        public List<SpecialtyData> Specializations { get; set; } = new List<SpecialtyData>();
    }
}
