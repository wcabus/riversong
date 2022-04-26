using Microsoft.EntityFrameworkCore;
using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Persistence.Repositories;

namespace RiverSong.Customers.Persistence.Repositories;

public class CustomerRepository : RepositoryBase<Customer, CustomersDbContext>, ICustomerRepository
{
    public CustomerRepository(CustomersDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsEmailAddressAlreadyInUseAsync(string emailAddress)
    {
        return await DbContext.Set<Customer>()
            .AnyAsync(x => x.Email == emailAddress);
    }
}