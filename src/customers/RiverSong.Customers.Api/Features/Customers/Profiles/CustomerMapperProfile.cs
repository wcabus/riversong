using RiverSong.Customers.Api.Features.Customers.Models;
using RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;
using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Customers.Application.Features.Customers.Queries.GetCustomers;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Api.Features.Customers.Profiles;

public class CustomerMapperProfile : AutoMapper.Profile
{
    public CustomerMapperProfile()
    {
        CreateMap<CustomersQueryParameters, GetCustomersQuery>()
            .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.GetPage()))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.GetPageSize()));

        CreateMap<CreateCustomer, CreateCustomerCommand>()
            .ConstructUsing(src => new CreateCustomerCommand(Guid.NewGuid(), src.FirstName, src.LastName, src.Email));

        CreateMap<CustomerDto, Customer>();
        CreateMap(typeof(Page<>), typeof(Shared.Api.Models.Page<>));
    }
}