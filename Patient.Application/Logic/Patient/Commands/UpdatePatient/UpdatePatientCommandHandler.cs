using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patient.Application.Common.Abstractions;
using Patient.Application.Common.Exceptions;
using Patient.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Application.Logic.Patient.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler : AbstractRequestHandler<UpdatePatientCommand, Guid>
    {
        public UpdatePatientCommandHandler(IMediator mediator, IPatientDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var data = await this.dbContext.Patients
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

            if (data is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Patient), request.Id);
            }

            data = this.mapper.Map(request, data);

            await this.dbContext.SaveChangesAsync(cancellationToken);

            return data.Id;
        }
    }
}
