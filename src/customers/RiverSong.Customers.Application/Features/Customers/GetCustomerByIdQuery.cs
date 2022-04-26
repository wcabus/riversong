using MediatR;
using RiverSong.Customers.Domain.Entities;

namespace RiverSong.Customers.Application.Features.Customers;

public class GetCustomerByIdQuery : IRequest<Customer?>
{
    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}