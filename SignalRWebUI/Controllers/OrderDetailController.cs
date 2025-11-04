using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalR.DtoLayer.OrderDetailDto;
using System.Net.Http;

namespace SignalRWebUI.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7195/api/OrderDetails/GetDetailsByOrderId/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

                var dataJson = jsonObject.data.ToString();

                var values = JsonConvert.DeserializeObject<List<ResultOrderDetailGetByOrderIdDto>>(dataJson);

                return View(values);
            }

            return View(new List<ResultOrderDetailGetByOrderIdDto>());
        }



    }
}
