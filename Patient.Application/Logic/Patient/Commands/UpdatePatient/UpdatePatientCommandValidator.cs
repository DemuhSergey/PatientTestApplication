using FluentValidation;
using Patient.Application.Common.Constants;

namespace Patient.Application.Logic.Patient.Commands.UpdatePatient
{
    public class UpdatePatientCommandValidator
        : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator() 
        {
            this.RuleFor(x => x.Family)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.Empty)
                .WithMessage(ValidationErrorMessages.Empty);

            this.RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodes.Empty)
                .WithMessage(ValidationErrorMessages.Empty);
        }
    }
}
