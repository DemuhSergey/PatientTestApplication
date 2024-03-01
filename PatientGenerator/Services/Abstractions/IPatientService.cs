using PatientGenerator.Models;

namespace PatientGenerator.Services.Abstractions
{
    internal interface IPatientService
    {
        Task<Guid[]> Insert(IEnumerable<Patient> list);
    }
}
