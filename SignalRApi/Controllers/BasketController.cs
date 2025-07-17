using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult GetBasketByMenuTableID(int id)
        {
            var values = _basketService.TGetBasketByRestaurantTableNumber(id);
            return Ok(values);
        }

        [HttpGet("BasketByRestaurantTableWithProductName")]
        public IActionResult BasketByRestaurantTableWithProductName(int id)
        {
            var context = new SignalRContext();
            var values = context.Baskets.Include(x=>x.Product).Where(y=>y.RestaurantTableID==id).Select(z=> new ResultBasketListWithProductName
            {
                BasketID = z.ProductID,
                RestaurantTableID = z.ProductID,
                Price=z.Price,
                Count=z.Count,
                ProductID=z.ProductID,
                TotalPrice=z.TotalPrice,
                ProductName = z.Product.ProductName
            }).ToList();
            return Ok(values);
        }
    }
}
