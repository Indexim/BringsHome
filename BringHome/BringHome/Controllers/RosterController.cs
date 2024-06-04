using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BringHome.Controllers
{
    public class RosterController : Controller
    {
        private  AppDBContext _db;

        public RosterController(AppDBContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Save([FromBody] Roster param)
        {
            _db.Add(param);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(new { status = StatusCodes.Status200OK, data = param });
            /*return Ok(new { Status = "Success" });*/
        }
    }
}
