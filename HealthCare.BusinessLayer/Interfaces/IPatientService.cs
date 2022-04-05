namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;

    using Contracts.Models.Appointment.Responses;
    using Contracts.Models.Common;
    using Contracts.Models.PatientAccount.Requests;
    using Contracts.Models.PatientAccount.Responses;

    public interface IPatientService
    {
        Task<TokenData> PersistMedicalProfile(PersistMedicalProfileRequest request);

        Task<GetPatientByEgnResponse> GetPatientByEng(long egn);
        
        Task<GetPatientAppointmentsResponse> GetAppointments(int patientInfoId);
    }
}