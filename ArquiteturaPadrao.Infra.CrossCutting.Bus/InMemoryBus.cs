using System.Collections.Generic;
using System.Threading.Tasks;
using ArquiteturaPadrao.Domain.Core.Bus;
using ArquiteturaPadrao.Domain.Core.Commands;
using ArquiteturaPadrao.Domain.Core.Events;
using ArquiteturaPadrao.Domain.Core.Queries;
using MediatR;

namespace ArquiteturaPadrao.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task<TReturn> SendQuery<TReturn>(QuerySingle<TReturn> querySingle) where TReturn : class
        {
            return _mediator.Send(querySingle);
        }

        public Task<IEnumerable<TReturn>> SendQueryCollection<TReturn>(QueryCollection<TReturn> queryCollection) where TReturn : class
        {
            return _mediator.Send(queryCollection);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
                _eventStore?.Save(@event);

            return _mediator.Publish(@event);
        }
    }
}