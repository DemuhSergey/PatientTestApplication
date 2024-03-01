using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patient.Application.Common.Abstractions;
using Patient.Application.Common.Exceptions;
using Patient.Domain.Abstractions.Interfaces;

namespace Patient.Application.Logic.Patient.Commands.RemovePatient
{
    public class RemovePatientCommandHandler : AbstractRequestHandler<RemovePatientCommand, Unit>
    {
        public RemovePatientCommandHandler(IMediator mediator, IPatientDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
        {
        }

        public override async Task<Unit> Handle(RemovePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await this.dbContext.Patients
               .Where(x => x.Id == request.Id)
               .FirstOrDefaultAsync(cancellationToken);

            if (patient is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Patient), request.Id);
            }

            this.dbContext.Patients.Remove(patient);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
