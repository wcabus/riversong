using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Persistence.Repositories;

namespace RiverSong.Customers.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CustomersDbContext>(x =>
        {
            x.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure());
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}