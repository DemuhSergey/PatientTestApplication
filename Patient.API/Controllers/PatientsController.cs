using Microsoft.AspNetCore.Mvc;
using Patient.API.Models;
using Patient.Application.Logic.Patient.Commands.CreatePatient;
using Patient.Application.Logic.Patient.Commands.RemovePatient;
using Patient.Application.Logic.Patient.Commands.UpdatePatient;
using Patient.Application.Logic.Patient.Models;
using Patient.Application.Logic.Patient.Queries.GetPatient;
using Patient.Application.Logic.Patient.Queries.GetPatientList;
using System.Net;

namespace Patient.API.Controllers
{
    [Route("patients")]
    public class PatientsController : BaseController
    {
        public PatientsController(ILogger<PatientsController> logger) : base(logger)
        {
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PatientInfo), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var service = await this.Mediator.Send(new GetPatientQuery(id));

            return this.Ok(service);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<PatientInfo>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList([FromQuery] string[] birthDate)
        {
            var servicesList = await this.Mediator.Send(new GetPatientListQuery(birthDate));

            return this.Ok(servicesList);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiValidationException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreatePatientCommand command)
        {
            var serviceId = await this.Mediator.Send(command);

            return this.Ok(serviceId);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiValidationException), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdatePatientCommand command)
        {
            command.Id = id;
            await this.Mediator.Send(command);

            return this.NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await this.Mediator.Send(new RemovePatientCommand { Id = id });

            return this.NoContent();
        }

    }
}
