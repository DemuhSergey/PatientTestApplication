using PatientGenerator.Services.Abstractions;
using RestSharp;
using System.Text.Json;

namespace PatientGenerator.Services
{
    internal class RestfullService<T> : IRestfullService<T>
    {
        public readonly string baseRoute = "https://localhost:8080/";

        public async Task<IEnumerable<T>> GetAll(string route)
        {
            var client = new RestClient(this.baseRoute);
            var request = new RestRequest(route);
            var response = await client.GetAsync<IEnumerable<T>>(request);

            return response?.Any() == true ? response : Enumerable.Empty<T>();
        }

        public Task<Guid> Post(string route, T data)
        {
            var json = JsonSerializer.Serialize(data);

            var client = new RestClient(this.baseRoute);
            var request = new RestRequest(route)
                .AddJsonBody(json);

            return client.PostAsync<Guid>(request);
        }

    }
}
