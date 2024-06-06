using Microsoft.AspNetCore.Mvc;

namespace BringHome.Controllers
{
    public class SparingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
