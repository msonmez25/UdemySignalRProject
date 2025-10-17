using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BasketDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class BasketsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            TempData["id"] = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7195/api/Basket/BasketByRestaurantTableWithProductName?id="+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDto>>(jsonData);
                TempData.Keep("id");
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> DeleteBasket(int id)
        {
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.DeleteAsync($"https://localhost:7195/api/Basket/{id}");
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", new { id = TempData["id"] });
            //}
            //else
            //{
            //    return NoContent();
            //}

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7195/api/Basket/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                int tableId = Convert.ToInt32(TempData["id"]);

                // Bu masaya ait kalan ürünleri kontrol et
                var basketResponse = await client.GetAsync($"https://localhost:7195/api/Basket/BasketByRestaurantTableWithProductName?id={tableId}");
                if (basketResponse.IsSuccessStatusCode)
                {
                    var jsonData = await basketResponse.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultBasketDto>>(jsonData);

                    // Eğer sepette ürün kalmadıysa masayı boş (false) yap
                    if (values == null || !values.Any())
                    {
                        await client.GetAsync($"https://localhost:7195/api/RestaurantTables/ChangeRestaurantTableStatusToFalse?id={tableId}");
                    }
                }

                return RedirectToAction("Index", new { id = tableId });
            }

            return NoContent();

        }

    }
}
