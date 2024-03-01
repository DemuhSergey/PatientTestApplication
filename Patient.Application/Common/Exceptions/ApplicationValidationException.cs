using FluentValidation.Results;
using Patient.Application.Common.Constants;

namespace Patient.Application.Common.Exceptions
{
    public class ApplicationValidationException : Exception
    {
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
                                    new ValidationError(x.ErrorCode ?? ValidationErrorCodes.UnrecognizedErrorCode,
                                        x.ErrorMessage ?? ValidationErrorMessages.UnrecognizedErrorCode)).ToArray());
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
                        new ValidationError(code ?? ValidationErrorCodes.UnrecognizedErrorCode,
                                            message ?? ValidationErrorMessages.UnrecognizedErrorCode)
                    }
            }
        };
        }
    }
}
