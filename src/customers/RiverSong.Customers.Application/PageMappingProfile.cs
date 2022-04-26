using RiverSong.Shared.Application.Models;

namespace RiverSong.Customers.Application;

public class PageMappingProfile : AutoMapper.Profile
{
    public PageMappingProfile()
    {
        CreateMap(typeof(Page<>), typeof(Page<>));
    }
}