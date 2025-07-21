using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;
using SignalR.EntityLayer.Entities;

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


        [HttpGet]
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
                BasketID = z.BasketID,
                RestaurantTableID = z.ProductID,
                Price=z.Price,
                Count=z.Count,
                ProductID=z.ProductID,
                TotalPrice=z.TotalPrice,
                ProductName = z.Product.ProductName
            }).ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
        {
            var context = new SignalRContext();
            _basketService.TAdd(new Basket()
            {
                ProductID = createBasketDto.ProductID,
                Count = 1,
                RestaurantTableID = 4,
                Price = context.Products.Where(x => x.ProductID == createBasketDto.ProductID).Select(y => y.Price).FirstOrDefault(),
                TotalPrice = 0
            });
            return Ok(); 
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var value = _basketService.TGetByID(id);
            _basketService.TDelete(value);
            return Ok("Sepette seçilen ürün silindi.");
        }

    }
}
