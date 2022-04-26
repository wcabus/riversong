using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, GetCustomersResponse>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Customer, bool>>? whereExpression = null;

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            whereExpression = customer => customer.FirstName.Contains(request.Search) || 
                                          customer.LastName.Contains(request.Search) ||
                                          customer.Email.Contains(request.Search);
        }

        var page = await _repository.GetPageAsync(request.Page, request.PageSize, whereExpression);
        return new GetCustomersResponse(_mapper.Map<Page<CustomerDto>>(page));
    }
}