using MediatR;
using Newtonsoft.Json;

namespace Patient.Application.Logic.Patient.Commands.RemovePatient
{
    public class RemovePatientCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id
        {
            get; set;
        }
    }
}
