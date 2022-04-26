using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RiverSong.Customers.Api.Features.Customers.Models;
using RiverSong.Customers.Api.Results;
using RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;
using RiverSong.Customers.Application.Features.Customers.Queries.GetCustomerById;
using RiverSong.Customers.Application.Features.Customers.Queries.GetCustomers;
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
    [Consumes(ContentTypes.Customers.Page, ContentTypes.Json)]
    [Produces(ContentTypes.Customers.Page, ContentTypes.Json, Type = typeof(Page<Customer>))]
    [ProducesResponseType(typeof(Page<Customer>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomers([FromQuery] CustomersQueryParameters parameters)
    {
        var response = await _mediator.Send(_mapper.Map<GetCustomersQuery>(parameters));

        return Ok(_mapper.Map<Page<Customer>>(response.Page));
    }

    [HttpGet("{id:guid}", Name = nameof(GetCustomerById))]
    [Consumes(ContentTypes.Customers.Single, ContentTypes.Json)]
    [Produces(ContentTypes.Customers.Single, ContentTypes.Json, Type = typeof(Customer))]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var response = await _mediator.Send(new GetCustomerByIdQuery(id));
        if (response.Customer is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Customer>(response.Customer));
    }

    [HttpPost]
    [Consumes(typeof(CreateCustomer), ContentTypes.Customers.Create, ContentTypes.Json)]
    [Produces(ContentTypes.Customers.Single, ContentTypes.Json, Type = typeof(Customer))]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IReadOnlyCollection<string>), StatusCodes.Status422UnprocessableEntity, ContentTypes.Errors, ContentTypes.Json)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer? model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        var response = await _mediator.Send(_mapper.Map<CreateCustomerCommand>(model));

        if (response.Succeeded)
        {
            return CreatedAtRoute(nameof(GetCustomerById), new { response.Customer!.Id }, _mapper.Map<Customer>(response.Customer));
        }

        return new ApiErrorResult(response.Errors);
    }
}