using System;
using System.Collections.Generic;
using ArquiteturaPadrao.Application.EventSourcedNormalizers;
using ArquiteturaPadrao.Application.Interfaces;
using ArquiteturaPadrao.Application.ViewModels;
using ArquiteturaPadrao.Domain.Core.Bus;
using ArquiteturaPadrao.Domain.Core.Interfaces;
using ArquiteturaPadrao.Domain.Core.Notifications;
using ArquiteturaPadrao.Domain.Core.Queries;
using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;
using ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces;
using ArquiteturaPadrao.Infra.Data.Repository.EventSourcing;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace ArquiteturaPadrao.Application.Services
{
    public class CustomerAppService : AppService, ICustomerAppService
    {
        public CustomerAppService(IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus,
            IMapper mapper,
            IEventStoreRepository eventStoreRepository) : base(uow, notifications, bus, mapper, eventStoreRepository)
        {
        }

        public  IEnumerable<CustomerViewModel> GetAll()
        {
            var customers = _bus.SendQueryCollection(new QueryCollection<CustomerViewModel>()).Result;
            return customers;
        }

        public CustomerViewModel GetById(Guid id)
        {
            var customers = _bus.SendQuery(new QuerySingle<CustomerViewModel>()).Result;
            return customers;
        }

        public void Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            _bus.SendCommand(registerCommand);
            Commit();
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            _bus.SendCommand(updateCommand);
            Commit();
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            _bus.SendCommand(removeCommand);
            Commit();
        }

        public IList<CustomerHistoryData> GetAllHistory(Guid id)
        {
            return CustomerHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));
        }
    }
}
