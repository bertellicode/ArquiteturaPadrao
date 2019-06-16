using System;
using System.Collections.Generic;
using System.Linq;
using ArquiteturaPadrao.Domain.Core.Events;
using ArquiteturaPadrao.Infra.Data.Context;

namespace ArquiteturaPadrao.Infra.Data.Repository.EventSourcing
{
    public class EventStoreSQLRepository : IEventStoreRepository
    {
        private readonly EventStoreSQLContext _context;

        public EventStoreSQLRepository(EventStoreSQLContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            var query = _context.StoredEvent.Where(e => e.AggregateId == aggregateId).ToList();
            return query;
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvent.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}