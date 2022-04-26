using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RiverSong.Customers.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var typeofThis = typeof(ApplicationServiceRegistration);
        services.AddAutoMapper(typeofThis);
        services.AddMediatR(typeofThis);

        return services;
    }
}