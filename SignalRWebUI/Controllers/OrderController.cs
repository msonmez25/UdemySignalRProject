using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
