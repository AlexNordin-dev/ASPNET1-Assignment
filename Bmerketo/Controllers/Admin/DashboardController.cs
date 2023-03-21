using Microsoft.AspNetCore.Mvc;

namespace Bmerketo.Controllers.Admin
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
