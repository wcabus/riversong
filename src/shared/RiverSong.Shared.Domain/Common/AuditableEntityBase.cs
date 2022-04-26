namespace RiverSong.Shared.Domain.Common;

public class AuditableEntityBase : EntityBase, IAuditableEntity
{
    public string CreatedBy { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}