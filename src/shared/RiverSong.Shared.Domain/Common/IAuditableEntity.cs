namespace RiverSong.Shared.Domain.Common;

public interface IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}