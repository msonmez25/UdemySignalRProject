using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
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
            var value = _mapper.Map<OpenInfo>(createOpenInfoDto);
            _openInfoService.TAdd(value);
            return Ok("Çalışma saat bilgisi eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOpenInfo(int id)
        {
            var value = _openInfoService.TGetByID(id);
            _openInfoService.TDelete(value);
            return Ok("Çalışma saat bilgisi silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetOpenInfo(int id)
        {
            var value = _openInfoService.TGetByID(id);
            return Ok(_mapper.Map<GetOpenInfoDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateOpenInfo(UpdateOpenInfoDto updateOpenInfoDto)
        {
            var value = _mapper.Map<OpenInfo>(updateOpenInfoDto);
            _openInfoService.TUpdate(value);
            return Ok("Çalışma saat bilgisi güncellendi.");
        }
    }
}
