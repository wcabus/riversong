using RiverSong.Customers.Application.Contracts;
using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Persistence.Repositories;

namespace RiverSong.Customers.Persistence.Repositories;

public class CustomerRepository : RepositoryBase<Customer, CustomersDbContext>, ICustomerRepository
{
    public CustomerRepository(CustomersDbContext dbContext) : base(dbContext)
    {
    }
}