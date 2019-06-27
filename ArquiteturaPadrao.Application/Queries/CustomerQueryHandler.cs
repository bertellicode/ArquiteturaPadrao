using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArquiteturaPadrao.Application.ViewModels;
using ArquiteturaPadrao.Domain.Core.Queries;
using ArquiteturaPadrao.Domain.Core.Queries.QueryHandlers;
using ArquiteturaPadrao.Domain.CustomerAggregate.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace ArquiteturaPadrao.Application.Queries
{
    public class CustomerQueryHandler : QueryHandler, 
                                        IRequestHandler<QueryCollection<CustomerViewModel>, IEnumerable<CustomerViewModel>>,
                                        IRequestHandler<QuerySingle<CustomerViewModel>, CustomerViewModel>

    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueryHandler(ICustomerRepository customerRepository,
                                    IMapper mapper) : base(mapper)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerViewModel>> Handle(QueryCollection<CustomerViewModel> request, CancellationToken cancellationToken)
        {
            var customers = _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);

            return customers;
        }

        public async Task<CustomerViewModel> Handle(QuerySingle<CustomerViewModel> request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var customer = _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));

            return customer;
        }
    }
}
