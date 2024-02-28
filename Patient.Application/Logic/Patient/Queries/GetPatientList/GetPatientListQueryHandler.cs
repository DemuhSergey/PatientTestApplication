using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patient.Application.Common.Abstractions;
using Patient.Application.Common.Exceptions;
using Patient.Application.Common.Extentions;
using Patient.Application.Logic.Patient.Models;
using Patient.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Application.Logic.Patient.Queries.GetPatientList
{
    public class GetPatientListQueryHandler
        : AbstractRequestHandler<GetPatientListQuery, IList<PatientInfo>>
    {
        public GetPatientListQueryHandler(IMediator mediator, IPatientDbContext dbContext, IMapper mapper) : base(mediator, dbContext, mapper)
        {
        }

        public override async Task<IList<PatientInfo>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dbContext.Patients
                .SearchByDate(request.Dates)
                .ProjectTo<PatientInfo>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (result is null)
            {
                result = new List<PatientInfo>();
            }

            return result;
        }
    }
}
