using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patient.Application.Common.Abstractions;
using Patient.Application.Common.Exceptions;
using Patient.Application.Logic.Patient.Models;
using Patient.Domain.Abstractions.Interfaces;

namespace Patient.Application.Logic.Patient.Queries.GetPatient
{
    public class GetPatientQueryHandler
        : AbstractRequestHandler<GetPatientQuery, PatientInfo>
    {
        public GetPatientQueryHandler(IMediator mediator, IPatientDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
        {
        }

        public override async Task<PatientInfo> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dbContext.Patients
                .Where(x => x.Id == request.Id)
                .ProjectTo<PatientInfo>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (result is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Patient), request.Id);
            }

            return result;
        }
    }
}
