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

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IRestaurantTableService restaurantTableService, IBookingService bookingService, INotificationService notificationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _restaurantTableService = restaurantTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }

        public static int clientCount { get; set; } = 0;

        public async Task SendStatistic()
        {
            //---------------------------------------------------------------------------
            //İlk Satır
            //Kategori Sayısı
            var value = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount",value);

            //Aktif Kategori Sayısı
            var value2 = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", value2);

            //Pasif Kategori Sayısı
            var value3 = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", value3);

            //Ürün Sayısı
            var value4 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", value4);

            //---------------------------------------------------------------------------
            //İkinci Satır
            //Hamburger Kategorisi Ürün Sayısı
            var value5 = _productService.TProductCountByCategoryNameHamburger();
            await Clients.All.SendAsync("ReceiveHamburgerProductCount", value5);


            //İçecek Kategorisi Ürün Sayısı
            var value6 = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveDrinkProductCount", value6);


            //Ortalama Ürün Fiyat
            var value7 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveAvgPriceProduct", value7.ToString("0.00") + " ₺");


            //Ortalama Hamburger Fiyat
            var value8 = _productService.TProductAvgPriceByHamburger();
            await Clients.All.SendAsync("ReceiveHamburgerAvgPriceProduct", value8.ToString("0.00") + " ₺");


            //---------------------------------------------------------------------------
            //Üçüncü Satır
            //En Ucuz Ürün
            var value9 = _productService.TProductNameByPriceMin();
            await Clients.All.SendAsync("ReceiveNameByPriceMin", value9);

            //En Pahalı Ürün
            var value10 = _productService.TProductNameByPriceMax();
            await Clients.All.SendAsync("ReceiveNameByPriceMax", value10);

            //Toplam Sipariş Sayısı
            var value11 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", value11);

            //Aktif Sipariş Sayısı
            var value12 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value12);


            //---------------------------------------------------------------------------
            //Dördüncü Satır
            //Son Sipariş Tutarı
            var value13 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", value13.ToString("0.00") + " ₺");

            //Kasadaki Tutar
            var value14 = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value14.ToString("0.00") + " ₺");


            //Bugünki Kazanç
            var value15 = _orderService.TTodayTotalPrice();
            await Clients.All.SendAsync("ReceiveTodayTotalPrice", value15.ToString("0.00") + " ₺");

            //Masa Sayısı
            var value16 = _restaurantTableService.TCountTable();
            await Clients.All.SendAsync("ReceiveCountTable", value16);
        }

        public async Task GetOrdersList()
        {
            //Siparişler Listesi
            var value = _orderService.TGetListAll();
            await Clients.All.SendAsync("ReceiveGetOrdersList", value);
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
            var value8 = _orderService.TActiveOrderCount();
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
