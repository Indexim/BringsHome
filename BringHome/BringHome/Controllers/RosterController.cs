using Microsoft.AspNetCore.Mvc;

namespace BringHome.Controllers
{
    public class RosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
