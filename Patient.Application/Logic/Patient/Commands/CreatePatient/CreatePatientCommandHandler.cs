using AutoMapper;
using MediatR;
using Patient.Application.Common.Abstractions;
using Patient.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Application.Logic.Patient.Commands.CreatePatient
{
    public class CreatePatientCommandHandler 
        : AbstractRequestHandler<CreatePatientCommand, Guid>
    {
        public CreatePatientCommandHandler(IMediator mediator, IPatientDbContext dbContext, IMapper mapper) 
            : base(mediator, dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var data = this.mapper.Map<Domain.Entities.Patient>(request);

            await this.dbContext.Patients.AddAsync(data, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return data.Id;
        }
    }
}
