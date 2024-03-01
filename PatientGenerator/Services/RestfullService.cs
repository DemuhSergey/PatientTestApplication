using Microsoft.Extensions.Logging;
using PatientGenerator.Exceptions;
using PatientGenerator.Services.Abstractions;
using RestSharp;
using System.Text.Json;

namespace PatientGenerator.Services
{
    internal class RestfullService<T> : IRestfullService<T>
    {
        //TODO: Move to appsetings.json
        public readonly string baseRoute = "http://localhost:8080";

        public async Task<Guid> Post(string route, T data)
        {
            var client = new RestClient(this.baseRoute);
            var request = new RestRequest(route, Method.Post);

            try
            {
                request.AddBody(data);
                var response = await client.PostAsync<Guid>(request);
                return response;

            }
            catch (Exception ex) { 
                throw new PatientGeneratorException(ex);
            }
        }
    }
}
