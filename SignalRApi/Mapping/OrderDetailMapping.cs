using AutoMapper;
using SignalR.DtoLayer.OrderDetailDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class OrderDetailMapping : Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, CreateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, ResultOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailDto>().ReverseMap();
        }
    }
}
