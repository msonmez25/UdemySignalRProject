using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    { 
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IRestaurantTableService _restaurantTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;
        private readonly IBasketService _basketService;
        private readonly IOrderDetailService  _orderDetailService;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IRestaurantTableService restaurantTableService, IBookingService bookingService, INotificationService notificationService, IBasketService basketService = null, IOrderDetailService orderDetailService = null)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _restaurantTableService = restaurantTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
            _basketService = basketService;
            _orderDetailService = orderDetailService;
        }

        public static int clientCount { get; set; } = 0;

        public async Task SendStatistic()
        {
            //---------------------------------------------------------------------------
            //İlk Satır
            //Bugünki Kazanç
            var value1 = _orderService.TTodayTotalPrice();
            await Clients.All.SendAsync("ReceiveTodayTotalPrice", value1.ToString("0.00") + " ₺");

            //Son Sipariş Tutarı
            var value2 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", value2.ToString("0.00") + " ₺");

            //Kasadaki Tutar
            var value3 = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value3.ToString("0.00") + " ₺");

            //Aktif Sipariş Sayısı
            var value4 = _basketService.TActiveBookingCount();
            await Clients.All.SendAsync("ReceiveActiveBookingCount", value4);

            //---------------------------------------------------------------------------
            //İkinci Satır

            //En Çok Sipariş Edilen Ürün
            var value5 = _orderDetailService.TMostOrderedProductName();
            await Clients.All.SendAsync("ReceiveMostOrderedProductName", value5);

            //En Çok Sipariş Veren Masa
            var value6 = _orderService.TMostOrderedTableName();
            await Clients.All.SendAsync("ReceiveMostOrderedTableName", value6);

            //Rezervasyon Sayısı
            var value7 = _bookingService.TTotalBookingCount();
            await Clients.All.SendAsync("ReceiveTotalBookingCount", value7);

            //Toplam Sipariş Sayısı
            var value8 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value8);


            //---------------------------------------------------------------------------
            //Üçüncü Satır

            //Ortalama Hamburger Fiyat
            var value9 = _productService.TProductAvgPriceByHamburger();
            await Clients.All.SendAsync("ReceiveHamburgerAvgPriceProduct", value9.ToString("0.00") + " ₺");

            //Ortalama Pizza
            var value10 = _productService.TProductAvgPriceByPizza();
            await Clients.All.SendAsync("ReceiveProductAvgPriceByPizza", value10.ToString("0.00") + " ₺");

            //Ortalama Makarna
            var value11 = _productService.TProductAvgPriceByPasta();
            await Clients.All.SendAsync("ReceiveProductAvgPriceByPasta", value11.ToString("0.00") + " ₺");

            //Ortalama Tatlı
            var value12 = _productService.TProductAvgPriceByDessert();
            await Clients.All.SendAsync("ReceiveProductAvgPriceByDessert", value12.ToString("0.00") + " ₺");


            //---------------------------------------------------------------------------
            //Dördüncü Satır

            //Kategori Sayısı
            var value13 = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount",value13);

            //Ürün Sayısı
            var value14 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", value14);   
            
            //En Ucuz Ürün
            var value15 = _productService.TProductNameByPriceMin();
            await Clients.All.SendAsync("ReceiveNameByPriceMin", value15);

            //En Pahalı Ürün
            var value16 = _productService.TProductNameByPriceMax();
            await Clients.All.SendAsync("ReceiveNameByPriceMax", value16);

            
        }

        public async Task GetOrdersList()
        {
            var orders = _orderService.TGetOrdersWithTableName();
            var result = orders.Select(o => new
            {
                orderID = o.OrderID,
                tableNumber = o.TableNumber,
                restaurantTableName = o.RestaurantTableName,
                totalPrice = o.TotalPrice,
                date = o.Date
            }).ToList();

            await Clients.All.SendAsync("ReceiveGetOrdersList", result);
        }

        public async Task SendProgress()
        {
            //Sol taraf
            //Son Sipariş Tutarı
            var value13 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", value13.ToString("0.00") + " ₺");

            //Bugünki Kazanç Tutarı
            var value14 = _orderService.TTodayTotalPrice();
            await Clients.All.SendAsync("ReceiveTodayTotalPrice", value14.ToString("0.00") + " ₺");

            //Kasadaki Toplam Tutar
            var value = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value.ToString("0.00") + " ₺");

            

            //------------------------------------------------------------------------
            //Orta Taraf
            //Toplam Masa Sayısı
            var value3 = _restaurantTableService.TCountTable();
            await Clients.All.SendAsync("ReceiveCountTable", value3);

            //Ortalama Ürün Fiyatı
            var value4 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveProductPriceAvg", value4);

            //Ortalama Hamburger Fiyatı
            var value5 = _productService.TProductAvgPriceByHamburger();
            await Clients.All.SendAsync("ReceiveProductAvgPriceByHamburger", value5);

            //Toplam İçecek Sayısı
            var value6 = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", value6);

            //Toplam Sipariş Sayısı
            var value7 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value7);
                        
            //Aktif Sipariş Sayısı
            var value8 = _basketService.TActiveBookingCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value8);

            //En Pahalı Ürün Fiyatı
            var value9 = _productService.TProductPriceMax();
            await Clients.All.SendAsync("ReceiveProductPriceMax", value9);

            //En Ucuz Ürün Fiyatı
            var value10 = _productService.TProductPriceMin();
            await Clients.All.SendAsync("ReceiveProductPriceMin", value10);

            //En Pahalı Ürün
            var value11 = _productService.TProductNameByPriceMax();
            await Clients.All.SendAsync("ReceiveProductNameByPriceMax", value11);

            //En Ucuz Ürün
            var value12 = _productService.TProductNameByPriceMin();
            await Clients.All.SendAsync("ReceiveProductNameByPriceMin", value12);

            //------------------------------------------------------------------------
            //Sağ Taraf
            //Aktif Sipariş Sayısı
            var value2 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value2);

            //Kategori Sayısı
            var value15 = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", value15);

            //Ürün Sayısı
            var value16 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", value16);

            //Rezervasyon Sayısı
            var value17 = _bookingService.TTotalBookingCount();
            await Clients.All.SendAsync("ReceiveTotalBookingCount", value17);

            //Onaylanmış Rezervasyon Sayısı
            var value18 = _bookingService.TOnaylanmisBookingCount();
            await Clients.All.SendAsync("ReceiveOnaylanmisBookingCount", value18);

            //İptal Edilmiş Rezervasyon Sayısı
            var value19 = _bookingService.TIptalEdilmisBookingCount();
            await Clients.All.SendAsync("ReceiveIptalEdilmisBookingCountt", value19);

            //Onaylanmamış Rezervasyon Sayısı
            var value20 = _bookingService.TOnaylanmamisBookingCount();
            await Clients.All.SendAsync("ReceiveOnaylanmamisBookingCount", value20);

        }

        public async Task GetBookingList()
        {
            var values = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", values);
        }

        public async Task SendNotification()
        {
            //---------------------------------------------------------------------------
            //Bildirim Sayısı
            var value = _notificationService.TNotificationCountByStatusFalse();
            await Clients.All.SendAsync("ReceiveNotificationCountByStatusFalse", value);

            //Bildirim Listesi
            var values = _notificationService.TGetAllNotificationByFalseList();
            await Clients.All.SendAsync("ReceiveGetAllNotificationByFalseList", values);

        }

        public async Task GetRestaurantTableStatus()
        {
            //Masaların Dolu - Boş Durumlarını Göstermek
            var value = _restaurantTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveGetRestaurantTableStatus",value);
        }

        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

       /* Bağlı olan client sayısını getirmek */
        public override async Task OnConnectedAsync()
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
