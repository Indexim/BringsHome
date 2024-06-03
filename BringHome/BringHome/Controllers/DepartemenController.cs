using Microsoft.AspNetCore.Mvc;

namespace BringHome.Controllers
{
    public class DepartemenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
