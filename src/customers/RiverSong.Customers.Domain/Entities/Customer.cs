using RiverSong.Shared.Domain.Common;

namespace RiverSong.Customers.Domain.Entities;

public class Customer : AuditableEntityBase
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}