using FluentValidation.Results;

namespace Patient.Application.Common.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        //TODO: Move to consts project
        public const string UnrecognizedErrorCode = "EC999999";
        public const string UnrecognizedErrorCodeMessage = "An error with unrecognized validation code occured";


        public IDictionary<string, ValidationError[]> Errors
        {
            get;
        }

        public ApplicationValidationException(IEnumerable<ValidationFailure> failures)
        {
            this.Errors = failures
                .GroupBy(e => e.PropertyName)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup =>
                                failureGroup.Select(x =>
                                    new ValidationError(x.ErrorCode ?? UnrecognizedErrorCode,
                                        x.ErrorMessage ?? UnrecognizedErrorCode)).ToArray());
        }

        public ApplicationValidationException(string propertyName, ValidationError validationError)
        {
            this.Errors = new Dictionary<string, ValidationError[]>()
        {
            {
                propertyName, new ValidationError[] { validationError }
            }
        };
        }

        public ApplicationValidationException(string propertyName, string code, string message)
        {
            this.Errors = new Dictionary<string, ValidationError[]>()
        {
            {
                propertyName,
                new ValidationError[]
                    {
                        new ValidationError(code ?? UnrecognizedErrorCode,
                                            message ?? UnrecognizedErrorCode)
                    }
            }
        };
        }
    }
}
