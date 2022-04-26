namespace RiverSong.Shared.Api.Models;

public record Page<T>(IReadOnlyCollection<T> Data, int PageIndex, int PageSize, int TotalItems);