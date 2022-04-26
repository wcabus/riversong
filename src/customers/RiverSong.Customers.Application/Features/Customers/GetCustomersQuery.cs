using MediatR;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application.Features.Customers;

public class GetCustomersQuery : IRequest<Page<Customer>>
{
    public string? Search { get; set; }
    public string? Includes { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}