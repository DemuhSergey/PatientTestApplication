using Newtonsoft.Json;
using Patient.Application.Common.Exceptions;

namespace Patient.API.Models
{
    public class ApiValidationException
    {
        [JsonProperty("errors")]
        public IDictionary<string, ValidationError[]> Errors
        {
            get;
        }

        public ApiValidationException(IDictionary<string, ValidationError[]> errors)
        {
            this.Errors = errors;
        }
    }
}
