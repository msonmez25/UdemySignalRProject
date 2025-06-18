using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var values = _aboutService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            About about = new About();
            {
                about.Title= createAboutDto.Title;
                about.Description= createAboutDto.Description;
                about.ImageUrl= createAboutDto.ImageUrl;
                about.Status = true;
            };
            _aboutService.TAdd(about);
            return Ok("Hakkımda alanı başarılı bir şekilde eklendi.");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var value=_aboutService.TGetByID(id);
            _aboutService.TDelete(value);
            return Ok("Hakkımda alanı silindi.");
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            About about = new About();
            {
                about.Title = updateAboutDto.Title;
                about.Description = updateAboutDto.Description;
                about.ImageUrl = updateAboutDto.ImageUrl;
                about.Status = true;
            };
            _aboutService.TUpdate(about);
            return Ok("Hakkımda alanı başarılı bir şekilde güncellendi.");
        }

        [HttpGet("GetAbout")]
        public IActionResult GetAbout(int id)
        {
            var value= _aboutService.TGetByID(id);
            return Ok(value);
        }

    }
}
