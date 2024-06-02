using Microsoft.AspNetCore.Mvc;

namespace indeapps.Controllers
{
    public class CaledarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
