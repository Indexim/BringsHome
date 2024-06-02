using Microsoft.AspNetCore.Mvc;

namespace indeapps.Controllers
{
    public class RosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
