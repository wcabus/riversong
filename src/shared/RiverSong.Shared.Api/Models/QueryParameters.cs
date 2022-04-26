namespace RiverSong.Shared.Api.Models;

public class QueryParameters
{
    private int _page = 1;
    private int _pageSize = 10;

    public string? Search { get; set; }
    public string? Includes { get; set; }

    public int? Page
    {
        get => _page;
        set
        {
            _page = value switch
            {
                null => 1,
                <= 1 => 1,
                _ => value.Value
            };
        }
    }

    public int? PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value switch
            {
                null => 10,
                <= 10 => 10,
                >= 100 => 100,
                _ => value.Value / 10 * 10
            };
        }
    }

    public int GetPage() => _page;
    public int GetPageSize() => _pageSize;
}