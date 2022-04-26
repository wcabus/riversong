using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdResponse : ResponseBase
{
    public GetCustomerByIdResponse(CustomerDto? customer)
    {
        Customer = customer;
    }

    public CustomerDto? Customer { get; }
}