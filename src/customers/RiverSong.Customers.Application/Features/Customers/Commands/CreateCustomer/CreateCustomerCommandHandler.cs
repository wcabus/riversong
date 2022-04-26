using AutoMapper;
using FluentValidation;
using MediatR;
using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Application.Features.Customers.Models;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Contracts;

namespace RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerCommandHandler(
        ICustomerRepository repository, 
        IMapper mapper,
        IUnitOfWork unitOfWork, 
        IValidator<CreateCustomerCommand> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new CreateCustomerResponse(validationResult);
        }

        await _repository.AddAsync(_mapper.Map<Customer>(request));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var customer = await _repository.GetByIdAsync(request.Id);
        return new CreateCustomerResponse(_mapper.Map<CustomerDto>(customer));
    }
}