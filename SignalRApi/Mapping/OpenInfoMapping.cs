using AutoMapper;
using SignalR.DtoLayer.OpenInfoDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class OpenInfoMapping : Profile
    {
        public OpenInfoMapping()
        {
            CreateMap<OpenInfo,CreateOpenInfoDto>().ReverseMap();
            CreateMap<OpenInfo,GetOpenInfoDto>().ReverseMap();
            CreateMap<OpenInfo,ResultOpenInfoDto>().ReverseMap();
            CreateMap<OpenInfo,UpdateOpenInfoDto>().ReverseMap();
        }
    }
}
