using ArquiteturaPadrao.Domain.Core.Events;
using System;

namespace ArquiteturaPadrao.Domain.CustomerAggregate.Events
{
    public class CustomerRemovedEvent : Event
    {
        public CustomerRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}