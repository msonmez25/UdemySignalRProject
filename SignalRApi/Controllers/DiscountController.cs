using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
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
            createDiscountDto.Status = false;
            var value = _mapper.Map<Discount>(createDiscountDto);
            _discountService.TAdd(value);
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
            return Ok(_mapper.Map<GetDiscountDto>(value));
        }

        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            updateDiscountDto.Status=false;
            var value = _mapper.Map<Discount>(updateDiscountDto);
            _discountService.TUpdate(value);
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

