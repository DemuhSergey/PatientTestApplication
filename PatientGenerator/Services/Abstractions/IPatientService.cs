using PatientGenerator.Models;

namespace PatientGenerator.Services.Abstractions
{
    internal interface IPatientService
    {
        Task<bool> HasExists();
        Task<Guid[]> Insert(IEnumerable<Patient> list);
    }
}
