using ArquiteturaPadrao.Domain.Core.Events;
using ArquiteturaPadrao.Infra.CrossCutting.JWT.Interfaces;
using ArquiteturaPadrao.Infra.Data.Repository.EventSourcing;
using Newtonsoft.Json;

namespace ArquiteturaPadrao.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUserProvider _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IUserProvider user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name);

            _eventStoreRepository.Store(storedEvent);
        }
    }
}