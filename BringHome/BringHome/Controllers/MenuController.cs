using Microsoft.AspNetCore.Mvc;

namespace BringHome.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
