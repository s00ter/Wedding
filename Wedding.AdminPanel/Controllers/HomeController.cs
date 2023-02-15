using Microsoft.AspNetCore.Mvc;

namespace Wedding.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
