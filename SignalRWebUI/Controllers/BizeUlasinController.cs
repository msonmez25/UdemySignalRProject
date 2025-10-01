using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.MessageDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class BizeUlasinController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BizeUlasinController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7195/api/Messages");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }


       

        public async Task<IActionResult> DeleteBizeUlasin(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7195/api/Messages/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "BizeUlasin");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateBizeUlasin(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7195/api/Messages/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateMessageDto>(jsonData);
                return View(values);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBizeUlasin(UpdateMessageDto updateMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7195/api/Messages", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "BizeUlasin");
            }
            return View();
        }


        public async Task<IActionResult> MessageStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7195/api/Messages/MessageStatusChangeToTrue/{id}");
            return RedirectToAction("Index", "BizeUlasin");
        }

        public async Task<IActionResult> MessageStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7195/api/Messages/MessageStatusChangeToFalse/{id}");
            return RedirectToAction("Index", "BizeUlasin");
        }
    }
}
