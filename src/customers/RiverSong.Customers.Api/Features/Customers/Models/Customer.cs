namespace RiverSong.Customers.Api.Features.Customers.Models;

public record Customer(Guid Id, string FirstName, string LastName, string Email);
public record CreateCustomer(string FirstName, string LastName, string Email);