using MediatR;

namespace RiverSong.Customers.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<GetCustomerByIdResponse>
{
    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}