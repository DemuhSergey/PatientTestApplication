using PatientGenerator.Models;
using PatientGenerator.Services.Abstractions;

namespace PatientGenerator.Services
{
    internal class PatientService : IPatientService
    {
        private readonly IRestfullService<Patient> restService;
        private readonly string controllerPath = "patient";
        public PatientService(
            IRestfullService<Patient> restFullPatientService) {

            restService = restFullPatientService;
        }
        public async Task<bool> HasExists()
        {
            var list = await restService.GetAll(this.controllerPath);
            return list.Any();
        }

        public Task<Guid[]> Insert(IEnumerable<Patient> list) =>
            Task.WhenAll(from patient in list select restService.Post(this.controllerPath, patient));
    }
}
