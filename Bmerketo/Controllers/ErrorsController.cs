using Microsoft.AspNetCore.Mvc;

namespace Bmerketo.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
