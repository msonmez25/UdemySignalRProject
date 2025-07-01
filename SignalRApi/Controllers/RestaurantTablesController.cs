using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;

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
    }
}
