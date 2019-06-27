using ArquiteturaPadrao.Domain.Core.Interfaces;
using ArquiteturaPadrao.Domain.CustomerAggregate.Models;

namespace ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}