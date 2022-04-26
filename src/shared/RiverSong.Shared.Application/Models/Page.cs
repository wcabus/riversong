namespace RiverSong.Shared.Application.Models
{
    public class Page<T> where T : class
    {
        public Page(IReadOnlyCollection<T> data, int pageIndex, int pageSize, int totalItems)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public IReadOnlyCollection<T> Data { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalItems { get; }
    }
}