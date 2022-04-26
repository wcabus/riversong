using AutoMapper;
using MediatR;
using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Application.Features.Customers.Models;

namespace RiverSong.Customers.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id);
        return new GetCustomerByIdResponse(_mapper.Map<CustomerDto>(customer));
    }
}