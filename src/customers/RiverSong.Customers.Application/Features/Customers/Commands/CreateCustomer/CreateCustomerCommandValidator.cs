using FluentValidation;
using RiverSong.Customers.Application.Contracts;

namespace RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(ICustomerRepository repository)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID must be set.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name must be set.")
            .MaximumLength(100).WithMessage("First name can be maximum {MaxLength} characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name must be set.")
            .MaximumLength(100).WithMessage("Last name can be maximum {MaxLength} characters long.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address must be set.")
            .MaximumLength(300).WithMessage("Email address can be maximum {MaxLength} characters long.")
            .EmailAddress().WithMessage("Email address must be a valid email.")
            .MustAsync(async (email, _) => !await repository.IsEmailAddressAlreadyInUseAsync(email))
                .WithMessage("Email address is already in use.");
    }
}