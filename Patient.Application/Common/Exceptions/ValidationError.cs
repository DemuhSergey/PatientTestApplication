using Newtonsoft.Json;

namespace Patient.Application.Common.Exceptions
{
    public class ValidationError
    {
        [JsonProperty("errorCode")]
        public string ErrorCode
        {
            get; set;
        }

        [JsonProperty("message")]
        public string Message
        {
            get; set;
        }

        public ValidationError(string code, string message)
        {
            this.ErrorCode = code;
            this.Message = message;
        }
    }
}
