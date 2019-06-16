using ArquiteturaPadrao.Domain.CustomerAggregate.Models;
using Equinox.Domain.Interfaces;

namespace ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}