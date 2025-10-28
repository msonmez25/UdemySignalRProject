using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.OrderDetailDto;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult OrderDetailList()
        {
            var values = _mapper.Map<List<ResultOrderDetailDto>>(_orderDetailService.TGetListAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            var value = _orderDetailService.TGetByID(id);
            return Ok(_mapper.Map<GetOrderDetailDto>(value));
        }

        [HttpGet("GetDetailsByOrderId/{orderId}")]
        public IActionResult GetDetailsByOrderId(int orderId)
        {
            try
            {
                var orderDetails = _orderDetailService.TGetOrderDetailsByOrderId(orderId);

                if (orderDetails == null || !orderDetails.Any())
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Bu siparişe ait detay bulunamadı."
                    });
                }

                var values = _mapper.Map<List<ResultOrderDetailGetByOrderIdDto>>(orderDetails);

                return Ok(new
                {
                    success = true,
                    message = "Sipariş detayları başarıyla getirildi.",
                    data = values
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Sipariş detayları alınırken hata oluştu: {ex.Message}"
                });
            }
        }

    }
}
