using System.Linq;
using System.Threading.Tasks;
using JPN.Core.DomainObjects;
using JPN.Core.Mediator;
using Microsoft.EntityFrameworkCore;

namespace JPN.Customer.API.Data
{
    public static class MediatorExtension
    {
        public static async Task PublishEvent<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.CleanEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}