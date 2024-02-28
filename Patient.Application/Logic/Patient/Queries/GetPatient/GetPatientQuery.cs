using MediatR;
using Patient.Application.Logic.Patient.Models;

namespace Patient.Application.Logic.Patient.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<PatientInfo>
    {
        public Guid Id
        {
            get;
        }

        public GetPatientQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
