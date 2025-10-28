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
            CreateMap<OrderDetail, ResultOrderDetailGetByOrderIdDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.Order.Date))
                .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Order.TableNumber))
                .ReverseMap();
        }
    }
}
