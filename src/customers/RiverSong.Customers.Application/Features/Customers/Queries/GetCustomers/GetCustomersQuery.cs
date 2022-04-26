using MediatR;

namespace RiverSong.Customers.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<GetCustomersResponse>
{
    public string? Search { get; set; }
    public string? Includes { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}