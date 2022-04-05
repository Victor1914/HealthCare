namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;
    using Contracts.Models.Common;

    public interface ICommonService
    {
        List<CityData> GetCities();

        List<SpecialtyData> GetSpecialties();

        List<CommonMedicalData> GetIllnesses(int categoryId);
       
        List<CommonMedicalData> GetIllnessCategories();

        List<VisitationDetailsData> GetVisitationReasons();
        
        List<CommonMedicalData> GetVisitationTypes();
    }
}
