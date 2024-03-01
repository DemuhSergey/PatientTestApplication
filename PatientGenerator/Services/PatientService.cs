using PatientGenerator.Models;
using PatientGenerator.Services.Abstractions;

namespace PatientGenerator.Services
{
    internal class PatientService : IPatientService
    {
        private readonly IRestfullService<Patient> restService;
        private readonly string controllerPath = "/patients";
        public PatientService(
            IRestfullService<Patient> restFullPatientService) {

            restService = restFullPatientService;
        }

        public Task<Guid[]> Insert(IEnumerable<Patient> list) =>
            Task.WhenAll(from patient in list select restService.Post(this.controllerPath, patient));
    }
}
