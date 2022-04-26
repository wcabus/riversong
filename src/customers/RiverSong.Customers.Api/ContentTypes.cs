namespace RiverSong.Customers.Api;

public static class ContentTypes
{
    public const string Json = "application/json";
    public const string Errors = "application/vnd.riversong.errors+json";

    public static class Customers
    {
        public const string Page = "application/vnd.riversong.customers.page+json";
        public const string Single = "application/vnd.riversong.customer+json";
        public const string Create = "application/vnd.riversong.customer.create+json";
    }
}