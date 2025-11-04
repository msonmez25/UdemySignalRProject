using AutoMapper;
using SignalR.DtoLayer.AboutDto;
using SignalR.DtoLayer.OrderDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, GetOrderDto>().ReverseMap();
            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, ResultOrdersWithTableNameDto>()
    .ForMember(dest => dest.RestaurantTableName,
               opt => opt.MapFrom(src => src.RestaurantTable.Name));

        }
    }
}
