using ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces;
using ArquiteturaPadrao.Domain.CustomerAggregate.Models;
using ArquiteturaPadrao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ArquiteturaPadrao.Infra.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ArquiteturaPadraoContext context)
            : base(context)
        {

        }

        public Customer GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
