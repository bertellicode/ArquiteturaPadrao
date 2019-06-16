using System;
using System.Collections.Generic;
using ArquiteturaPadrao.Application.EventSourcedNormalizers;
using ArquiteturaPadrao.Application.Interfaces;
using ArquiteturaPadrao.Application.ViewModels;
using ArquiteturaPadrao.Domain.Core.Bus;
using ArquiteturaPadrao.Domain.Core.Notifications;
using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;
using ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces;
using ArquiteturaPadrao.Infra.Data.Repository.EventSourcing;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Domain.Interfaces;

namespace ArquiteturaPadrao.Application.Services
{
    public class CustomerAppService : AppService, ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerAppService(IUnitOfWork uow,
            DomainNotificationHandler notifications,
            IMediatorHandler bus,
            IMapper mapper,
            IEventStoreRepository eventStoreRepository,
            ICustomerRepository customerRepository) : base(uow, notifications, bus, mapper, eventStoreRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }

        public CustomerViewModel GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
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
