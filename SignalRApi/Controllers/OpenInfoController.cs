using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.OpenInfoDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenInfoController : ControllerBase
    {
        private readonly IOpenInfoService _openInfoService;
        private readonly IMapper _mapper;

        public OpenInfoController(IOpenInfoService openInfoService, IMapper mapper)
        {
            _openInfoService = openInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult OpenInfoList()
        {
            var value = _mapper.Map<List<ResultOpenInfoDto>>(_openInfoService.TGetListAll());
            return Ok(value);
        }


        [HttpPost]
        public IActionResult CreateOpenInfo(CreateOpenInfoDto createOpenInfoDto)
        {
            _openInfoService.TAdd(new OpenInfo()
            {
               Title= createOpenInfoDto.Title,
               DateFirst= createOpenInfoDto.DateFirst,
               DateEnd= createOpenInfoDto.DateEnd,

            });
            return Ok("Çalışma saat bilgisi eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteOpenInfo(int id)
        {
            var value = _openInfoService.TGetByID(id);
            _openInfoService.TDelete(value);
            return Ok("Çalışma saat bilgisi silindi");
        }

        [HttpGet("GetOpenInfo")]
        public IActionResult GetOpenInfo(int id)
        {
            var value = _openInfoService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateOpenInfo(UpdateOpenInfoDto updateOpenInfoDto)
        {
            _openInfoService.TUpdate(new OpenInfo()
            {
                OpenInfoID = updateOpenInfoDto.OpenInfoID,
                Title = updateOpenInfoDto.Title,
                DateFirst = updateOpenInfoDto.DateFirst,
                DateEnd = updateOpenInfoDto.DateEnd,
            });
            return Ok("Çalışma saat bilgisi güncellendi.");
        }
    }
}
