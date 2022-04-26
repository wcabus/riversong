using RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;
using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Customers.Domain.Entities;

namespace RiverSong.Customers.Application.Features.Customers;

public class CustomerMappingProfile : AutoMapper.Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();

        CreateMap<Customer, CustomerDto>();
    }
}