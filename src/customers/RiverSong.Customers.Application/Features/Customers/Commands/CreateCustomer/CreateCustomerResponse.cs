using FluentValidation.Results;
using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerResponse : ResponseBase
{
    public CreateCustomerResponse(CustomerDto customer)
    {
        Customer = customer;
    }

    public CreateCustomerResponse(ValidationResult validationResult) 
        : base("Validation failed", validationResult.Errors.Select(x => x.ErrorMessage).ToArray()) {}

    public CustomerDto? Customer { get; }
}