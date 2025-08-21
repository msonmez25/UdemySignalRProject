using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.DiscountDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DiscountList()
        {
            var value = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetListAll());
            return Ok(value);
        }

        [HttpGet("GetDiscountListByStatusTrue")]
        public IActionResult GetDiscountListByStatusTrue()
        {
            var value = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetDiscountListByStatusTrue());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            _discountService.TAdd(new Discount()
            {
               Title= createDiscountDto.Title,
               Amount= createDiscountDto.Amount,
               Description= createDiscountDto.Description,
               ImageUrl= createDiscountDto.ImageUrl,
               Status = false,

            });
            return Ok("İndirim bilgisi eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            _discountService.TDelete(value);
            return Ok("İndirim bilgisi silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            _discountService.TUpdate(new Discount()
            {
                DiscountID = updateDiscountDto.DiscountID,
                Title = updateDiscountDto.Title,
                Amount = updateDiscountDto.Amount,
                Description = updateDiscountDto.Description,
                ImageUrl = updateDiscountDto.ImageUrl,
                Status = false,
            });
            return Ok("İndirim bilgisi güncellendi.");
        }



        [HttpGet("DisCountStatusChangeTrue/{id}")]
        public IActionResult DisCountStatusChangeTrue(int id)
        {
            _discountService.TDisCountStatusChangeTrue(id);
            return Ok("İndirimli Ürün Aktif Olarak Düzenlendi");
        }

        [HttpGet("DisCountStatusChangeFalse/{id}")]
        public IActionResult DisCountStatusChangeFalse(int id)
        {
            _discountService.TDisCountStatusChangeFalse(id);
            return Ok("İndirimli Ürün Pasif Olarak Düzenlendi");
        }
    }
}

