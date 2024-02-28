using AutoMapper;
using MediatR;
using Patient.Domain.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Application.Common.Abstractions
{
    public abstract class AbstractRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator mediator;
        protected readonly IPatientDbContext dbContext;
        protected readonly IMapper mapper;


        public AbstractRequestHandler(
            IMediator mediator,
            IPatientDbContext dbContext,
            IMapper mapper
            )
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
