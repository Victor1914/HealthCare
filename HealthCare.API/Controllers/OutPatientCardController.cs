namespace HealthCare.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Behaviour.Filters;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.OutpatientCard.Data;
    using Contracts.Models.OutpatientCard.Requests;
    using Contracts.Models.OutpatientCard.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Enums;

    [ApiController]
    [ValidationFilter]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Doctor)]
    [Route("outpatientCard")]
    public class OutPatientCardController : ControllerBase
    {
        private readonly IOutPatientCardService _outPatientCardService;

        public OutPatientCardController(IOutPatientCardService outPatientCardService)
        {
            _outPatientCardService = outPatientCardService;
        }

        [Route("add")]
        [HttpPost]
        public async Task<TokenData> PersistOutpatientCard(
            [ModelBinder(BinderType = typeof(JsonModelBinder))] AddOutpatientCardRequest request, [FromForm] IList<IFormFile> file)
        {
            return await _outPatientCardService.Add(request, file);
        }

        [Route("getAll/{patientId}")]
        [HttpGet]
        public GetOutPatientCardsResponse GetAll(int patientId)
        {
            return _outPatientCardService.GetAllByPatient(patientId);
        }
    }
}
