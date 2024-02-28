using MediatR;
using Patient.Application.Logic.Patient.Models;

namespace Patient.Application.Logic.Patient.Queries.GetPatientList
{
    public class GetPatientListQuery : IRequest<IList<PatientInfo>>
    {

        public IEnumerable<string> Dates { get; }

        public GetPatientListQuery(string[] date)
        {
            this.Dates = date;
        }
    }
}
