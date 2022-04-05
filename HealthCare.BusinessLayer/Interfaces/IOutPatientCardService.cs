namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    using Contracts.Models.Common;
    using Contracts.Models.OutpatientCard.Data;
    using Contracts.Models.OutpatientCard.Requests;
    using Contracts.Models.OutpatientCard.Responses;
    
    public interface IOutPatientCardService
    {
        Task<TokenData> Add(AddOutpatientCardRequest request, IList<IFormFile> files);

        GetOutPatientCardsResponse GetAllByPatient(int patientInfoId);
    }
}