namespace RiverSong.Shared.Application.Models;

public abstract class ResponseBase
{
    protected ResponseBase()
    {
    }

    protected ResponseBase(string errorMessage, IReadOnlyCollection<string> errors)
    {
        ErrorMessage = errorMessage;
        Errors = errors;
    }

    public bool Succeeded => string.IsNullOrEmpty(ErrorMessage);

    public string? ErrorMessage { get; protected set; }
    public IReadOnlyCollection<string>? Errors { get; protected set; }
}