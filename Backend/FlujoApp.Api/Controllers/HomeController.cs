using Microsoft.AspNetCore.Mvc;

namespace FlujoApp.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
