using AutoMapper;
using SignalR.DtoLayer.RestaurantTableDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class RestaurantTableMapping : Profile
    {
        public RestaurantTableMapping()
        {
            CreateMap<RestaurantTable, CreateRestaurantTableDto>().ReverseMap();
            CreateMap<RestaurantTable, GetRestaurantTableDto>().ReverseMap();
            CreateMap<RestaurantTable, ResultRestaurantTableDto>().ReverseMap();
            CreateMap<RestaurantTable, UpdateRestaurantTableDto>().ReverseMap();
        }
    }
}
