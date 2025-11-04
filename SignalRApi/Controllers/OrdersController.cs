using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.EntityLayer.Entities;
using SignalR.DtoLayer.OrderDto;


namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;
        private readonly SignalRContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMoneyCaseService _moneyCaseService;

        public OrdersController(IOrderService orderService, IMapper mapper, IBasketService basketService, SignalRContext context, IHttpClientFactory httpClientFactory, IMoneyCaseService moneyCaseService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _basketService = basketService;
            _context = context;
            _httpClientFactory = httpClientFactory;
            _moneyCaseService = moneyCaseService;
        }


        [HttpGet]
        public IActionResult OrderList()
        {
            var values = _mapper.Map<List<ResultOrderDto>>(_orderService.TGetListAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var value = _orderService.TGetByID(id);
            return Ok(_mapper.Map<GetOrderDto>(value));
        }


        [HttpPost("CreateOrderFromBasket")]
        public async Task<IActionResult> CreateOrderFromBasket(int tableId)
        {
            const decimal KdvOrani = 0.10m; // %10 KDV

            // Sepetteki ürünleri al
            var basketItems = await _context.Baskets
                .Include(x => x.Product)
                .Where(x => x.RestaurantTableID == tableId)
                .ToListAsync();

            if (basketItems == null || !basketItems.Any())
                return BadRequest("Sepet boş, sipariş oluşturulamadı.");

            // Sipariş oluştur
            var order = new Order
            {
                TableNumber = tableId.ToString(),
                Description = "Yeni sipariş oluşturuldu.(KDV dahil)",
                Date = DateTime.Now,
                orderDetails = new List<OrderDetail>(),
                RestaurantTableID = tableId,
                
            };

            decimal toplamTutar = 0;

            // Sepet ürünlerini OrderDetail olarak ekle (KDV dahil)
            foreach (var item in basketItems)
            {
                decimal kdvDahilFiyat = item.TotalPrice + (item.TotalPrice * KdvOrani);

                order.orderDetails.Add(new OrderDetail
                {
                    ProductID = item.ProductID,
                    Count = item.Count,
                    UnitPrice = item.Price,
                    TotalPrice = kdvDahilFiyat
                });

                toplamTutar += kdvDahilFiyat;
            }

            order.TotalPrice = toplamTutar;

            // Siparişi kaydet
            _orderService.TAdd(order);

            // Kasa tablosuna toplam tutarı ekle
            var moneyCase = new MoneyCase
            {
                TotalAmount = order.TotalPrice
            };
            _moneyCaseService.TAdd(moneyCase);

            // Sepeti temizle
            foreach (var item in basketItems)
            {
                _basketService.TDelete(item);
            }

            // Masayı boş duruma getir (API üzerinden)
            var client = new HttpClient();
            await client.GetAsync($"https://localhost:7195/api/RestaurantTables/ChangeRestaurantTableStatusToFalse?id={tableId}");

            return Ok("Sipariş başarıyla oluşturuldu (KDV dahil), sepet temizlendi ve masa boş duruma getirildi.");

        }


        [HttpGet("OrdersWithTableName")]
        public IActionResult OrdersWithTableName()
        {
            var context = new SignalRContext();
            var values = context.Orders.Include(x => x.RestaurantTable).Select(y => new ResultOrdersWithTableNameDto
            {
                OrderID = y.OrderID,
                TableNumber = y.TableNumber,
                Description = y.Description,
                Date = y.Date,
                TotalPrice= y.TotalPrice,
                RestaurantTableName=y.RestaurantTable.Name
                
            });
            return Ok(values);
        }



        [HttpGet("TotalOrderCount")]
        public IActionResult TotalOrderCount()
        {
            return Ok(_orderService.TTotalOrderCount());
        }


        [HttpGet("ActiveOrderCount")]
        public IActionResult ActiveOrderCount()
        {
            return Ok(_orderService.TActiveOrderCount());
        }


        [HttpGet("LastOrderPrice")]
        public IActionResult LastOrderPrice()
        {
            return Ok(_orderService.TLastOrderPrice());
        }

        [HttpGet("TodayTotalPrice")]
        public IActionResult TodayTotalPrice()
        {
            return Ok(_orderService.TTodayTotalPrice());
        }

        
    }
}
