using System;
using System.Collections.Generic;
using ArquiteturaPadrao.Application.EventSourcedNormalizers;
using ArquiteturaPadrao.Application.ViewModels;

namespace ArquiteturaPadrao.Application.Interfaces
{
    public interface ICustomerAppService : IAppService
    {
        void Register(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(Guid id);
        void Update(CustomerViewModel customerViewModel);
        void Remove(Guid id);
        IList<CustomerHistoryData> GetAllHistory(Guid id);
    }
}
