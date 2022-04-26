using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RiverSong.Customers.Api.Features.Customers.Models;
using RiverSong.Customers.Application.Features.Customers;
using RiverSong.Shared.Api.Models;

namespace RiverSong.Customers.Api.Features.Customers;

[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CustomersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [Consumes("application/vnd.riversong.customers.page+json", "application/json")]
    [Produces("application/vnd.riversong.customers.page+json", "application/json", Type = typeof(Page<Customer>))]
    public async Task<IActionResult> GetCustomers([FromQuery] CustomersQueryParameters parameters)
    {
        var response = await _mediator.Send(_mapper.Map<GetCustomersQuery>(parameters));

        return Ok(_mapper.Map<Page<Customer>>(response));
    }

    [HttpGet("{id:guid}", Name = nameof(GetCustomerById))]
    [Consumes("application/vnd.riversong.customer+json", "application/json")]
    [Produces("application/vnd.riversong.customer+json", "application/json", Type = typeof(Customer))]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery(id));
        if (response is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Customer>(response));
    }
}