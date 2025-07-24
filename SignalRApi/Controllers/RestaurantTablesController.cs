using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
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
            _restaurantTableService.TAdd(new RestaurantTable()
            {
                Name = createRestaurantTableDto.Name,
                Status = false,

            });
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
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateRestaurantTable(UpdateRestaurantTableDto updateRestaurantTableDto)
        {
            _restaurantTableService.TUpdate(new RestaurantTable()
            {
                RestaurantTableID= updateRestaurantTableDto.RestaurantTableID,
                Name= updateRestaurantTableDto.Name,
                Status = updateRestaurantTableDto.Status
                
            });
            return Ok("Masa güncellendi.");
        }
    }
}
