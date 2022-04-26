using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersResponse : ResponseBase
{
    public GetCustomersResponse(Page<CustomerDto> page)
    {
        Page = page;
    }

    public Page<CustomerDto> Page { get; }
}