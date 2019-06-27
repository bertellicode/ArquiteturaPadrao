using System;
using ArquiteturaPadrao.Application.Interfaces;
using ArquiteturaPadrao.Domain.Core.Bus;
using ArquiteturaPadrao.Domain.Core.Interfaces;
using ArquiteturaPadrao.Domain.Core.Notifications;
using ArquiteturaPadrao.Infra.Data.Repository.EventSourcing;
using AutoMapper;
using MediatR;

namespace ArquiteturaPadrao.Application.Services
{
    public class AppService : IAppService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _bus;
        protected readonly IMapper _mapper;
        protected readonly IEventStoreRepository _eventStoreRepository;

        public AppService(IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler bus,
            IMapper mapper,
            IEventStoreRepository eventStoreRepository)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
            _mapper = mapper;
            _eventStoreRepository = eventStoreRepository;
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_uow.Commit()) return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
