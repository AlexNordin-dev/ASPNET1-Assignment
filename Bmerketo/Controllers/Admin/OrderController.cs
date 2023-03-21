using Microsoft.AspNetCore.Mvc;

namespace Bmerketo.Controllers.Admin
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
