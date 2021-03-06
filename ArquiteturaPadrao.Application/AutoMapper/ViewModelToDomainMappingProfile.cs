﻿using ArquiteturaPadrao.Application.ViewModels;
using ArquiteturaPadrao.Domain.CustomerAggregate.Commands;
using AutoMapper;

namespace ArquiteturaPadrao.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id.Value, c.Name, c.Email, c.BirthDate));
        }
    }
}
