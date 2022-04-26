using RiverSong.Customers.Domain.Entities;
using RiverSong.Shared.Application.Contracts;

namespace RiverSong.Customers.Application.Contracts;

public interface ICustomerRepository : IAsyncRepository<Customer>
{
    Task<bool> IsEmailAddressAlreadyInUseAsync(string emailAddress);
}