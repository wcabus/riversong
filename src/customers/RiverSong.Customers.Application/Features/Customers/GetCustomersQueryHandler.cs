using System.Linq.Expressions;
using MediatR;
using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application.Features.Customers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Page<Customer>>
{
    private readonly ICustomerRepository _repository;

    public GetCustomersQueryHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Page<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Customer, bool>>? whereExpression = null;

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            whereExpression = customer => customer.FirstName.Contains(request.Search) || 
                                          customer.LastName.Contains(request.Search) ||
                                          customer.Email.Contains(request.Search);
        }

        return await _repository.GetPageAsync(request.Page, request.PageSize, whereExpression);
    }
}