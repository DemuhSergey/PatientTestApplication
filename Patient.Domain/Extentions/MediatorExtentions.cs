using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Patient.Domain.Extentions
{
    public static class MediatorExtentions
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, DbContext dbContext)
        {
            var entities = dbContext.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity);

            var domainEvents = entities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            entities.ToList().ForEach(x => x.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
