using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Patient.Domain.Abstractions.Interfaces;
using Patient.Domain.Extentions;

namespace Patient.Persistence
{
    public class PatientDbContext
        : DbContext, IPatientDbContext
    {
        private readonly IMediator _mediator;

        public PatientDbContext(
            DbContextOptions<PatientDbContext> options,
            IMediator mediator)
            : base(options)
        {
            this._mediator = mediator;
        }

        public DbSet<Domain.Entities.Patient> Patients => this.Set<Domain.Entities.Patient>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await this._mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
