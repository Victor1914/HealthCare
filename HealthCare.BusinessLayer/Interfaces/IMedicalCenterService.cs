namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;
    using Contracts.Models.Common;
    using Contracts.Models.MedicalCenterAccount.Requests;
    using Contracts.Models.MedicalCenterAccount.Responses;

    public interface IMedicalCenterService
    {
        Task<TokenData> PersistMedicalCenterData(PersistMedicalCenterDataRequest request);

        GetMedicalCentersResponse GetMedicalCenters(int? cityId);

        Task<GetMedicalCentersResponse> GetMedicalCentersForDoctor();
    }
}
