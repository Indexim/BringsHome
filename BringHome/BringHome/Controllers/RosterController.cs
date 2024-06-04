using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BringHome.Controllers
{
    public class RosterController : Controller
    {
        private readonly AppDBContext _db;

        public RosterController(AppDBContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            try
            {
                var results = await _db.vw_roster_karyawan.OrderBy(x => x.nama).ToListAsync();
                return Json(new { status = StatusCodes.Status200OK, message = "success", data = results });
            }
            catch (Exception e)
            {
                return Json(new { status = StatusCodes.Status204NoContent, message = "Gagal", data = e.Message });
            }
        }

        public async Task<IActionResult> GetRosters()
        {
            try
            {
                var res = await _db.roster_karyawan.ToListAsync();
                return Ok( new { status = StatusCodes.Status200OK, message = "success", data = res });
            }
            catch (Exception)
            {
                throw;
            }
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

        public IActionResult Detail(string id)
        {
            var data = _db.vw_roster_karyawan.FromSql($"SELECT * FROM tbl_m_karyawan").ToList();
            return View();
        }

        [HttpGet("GetRosterByNik/{id}")]
        public async Task<IActionResult> GetRosterByNik(string id)
        {
            return Ok();
        }
    }
}
