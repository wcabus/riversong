using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RiverSong.Customers.Application.Features.Customers.Commands.CreateCustomer;

namespace RiverSong.Customers.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var typeofThis = typeof(ApplicationServiceRegistration);
        services.AddAutoMapper(typeofThis);
        services.AddMediatR(typeofThis);

        services.AddTransient<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();

        return services;
    }
}