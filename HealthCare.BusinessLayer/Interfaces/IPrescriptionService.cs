namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;
   
    using Contracts.Models.Common;
    using Contracts.Models.Prescription.Requests;
    using Contracts.Models.Prescription.Responses;

    public interface IPrescriptionService
    {
        public Task<TokenData> AddPrescription(AddPrescriptionRequest prescription);

        public Task<GetPrescriptionsResponse> GetPatientPrescriptions(int patientId);
    }
}