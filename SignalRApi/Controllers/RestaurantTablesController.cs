using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
using SignalR.DtoLayer.RestaurantTableDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTablesController : ControllerBase
    {
        private readonly IRestaurantTableService _restaurantTableService;
        private readonly IMapper _mapper;

        public RestaurantTablesController(IRestaurantTableService restaurantTableService, IMapper mapper)
        {
            _restaurantTableService = restaurantTableService;
            _mapper = mapper;
        }


        [HttpGet("CountTable")]
        public IActionResult CountTable()
        {
            return Ok(_restaurantTableService.TCountTable());
        }


        [HttpGet]
        public IActionResult TestimonialList()
        {
            var value = _mapper.Map<List<ResultRestaurantTableDto>>(_restaurantTableService.TGetListAll());
            return Ok(value);
        }


        [HttpPost]
        public IActionResult CreateRestaurantTable(CreateRestaurantTableDto createRestaurantTableDto)
        {
            createRestaurantTableDto.Status = true;
            var value = _mapper.Map<RestaurantTable>(createRestaurantTableDto);
            _restaurantTableService.TAdd(value);
            return Ok("Masa eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurantTable(int id)
        {
            var value = _restaurantTableService.TGetByID(id);
            _restaurantTableService.TDelete(value);
            return Ok("Masa silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurantTable(int id)
        {
            var value = _restaurantTableService.TGetByID(id);
            return Ok(_mapper.Map<GetRestaurantTableDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateRestaurantTable(UpdateRestaurantTableDto updateRestaurantTableDto)
        {
            var value = _mapper.Map<RestaurantTable>(updateRestaurantTableDto);
            _restaurantTableService.TUpdate(value);
            return Ok("Masa güncellendi.");
        }

        [HttpGet("ChangeRestaurantTableStatusToTrue")]
        public IActionResult ChangeRestaurantTableStatusToTrue(int id)
        {
            _restaurantTableService.TChangeRestaurantTableStatusToTrue(id);
            return Ok("Masa durum True yapıldı.");
        }

        [HttpGet("ChangeRestaurantTableStatusToFalse")]
        public IActionResult ChangeRestaurantTableStatusToFalse(int id)
        {
            _restaurantTableService.TChangeRestaurantTableStatusToFalse(id);
            return Ok("Masa durum False yapıldı.");
        }
    }
}
