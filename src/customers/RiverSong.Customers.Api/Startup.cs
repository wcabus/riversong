using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RiverSong.Customers.Api.Results;
using RiverSong.Customers.Api.Services;
using RiverSong.Customers.Application;
using RiverSong.Customers.Persistence;
using RiverSong.Shared.Application.Contracts;

namespace RiverSong.Customers.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddTransient<IActionResultExecutor<ApiErrorResult>, ApiErrorResultExecutor>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Plumbing
        services.AddApplicationServices();
        services.AddPersistenceServices(Configuration.GetConnectionString("CustomersDb"));

        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddAutoMapper(typeof(Startup));
        services.AddFluentValidation();
    }

    public void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}