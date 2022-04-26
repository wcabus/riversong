namespace RiverSong.Shared.Application.Contracts;

public interface IUserAccessor
{
    Task<string> GetCurrentUserName();
}