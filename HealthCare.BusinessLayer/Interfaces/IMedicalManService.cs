namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contracts.Models.Common;
    using Contracts.Models.MedicalManAccount.Data;
    using Contracts.Models.MedicalManAccount.Requests;
    using Contracts.Models.MedicalManAccount.Responses;
   

    public interface IMedicalManService
    {
        Task<TokenData> PersistPersonalData(PersistPersonalDataRequest request);

        Task<RetrieveMainDataResponse> RetrievePersonalData();

        Task<RetrieveContactsResponse> RetrieveContacts();

        List<CommonMedicalData> GetByMedicalCenter(int centerId);
       
        StatisticsData GetStatisticsData();
    }
}
