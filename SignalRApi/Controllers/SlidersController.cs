using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.SliderDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SlidersController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult SliderList()
        {
            var value = _mapper.Map<List<ResultSliderDto>>(_sliderService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("{id}")]
        public IActionResult GetSlider(int id)
        {
            var value = _sliderService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateSlider(UpdateSliderDto updatesliderDto)
        {
            _sliderService.TUpdate(new Slider()
            {
                SliderID = updatesliderDto.SliderID,
                Title1 = updatesliderDto.Title1,
                Description1 = updatesliderDto.Description1,
                Title2 = updatesliderDto.Title2,
                Description2 = updatesliderDto.Description2,
                Title3 = updatesliderDto.Title3,
                Description3 = updatesliderDto.Description3

            });
            return Ok("Slider bilgisi güncellendi.");
        }
    }
}
