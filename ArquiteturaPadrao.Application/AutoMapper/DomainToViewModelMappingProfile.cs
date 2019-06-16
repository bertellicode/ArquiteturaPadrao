using ArquiteturaPadrao.Application.ViewModels;
using ArquiteturaPadrao.Domain.CustomerAggregate.Models;
using AutoMapper;

namespace ArquiteturaPadrao.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
