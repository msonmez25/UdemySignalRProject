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

        public async Task SendProgress()
        {
            //Kasadaki Toplam Tutar
            var value = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value.ToString("0.00") + " ₺");

            //Akitf Sipariş Sayısı
            var value2 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value2);

            //Toplam Masa Sayısı
            var value3 = _restaurantTableService.TCountTable();
            await Clients.All.SendAsync("ReceiveCountTable", value3);
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
            var value = _restaurantTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveGetRestaurantTableStatus",value);
        }
    }
}
