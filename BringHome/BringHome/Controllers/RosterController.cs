using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BringHome.Controllers
{
    public class RosterController : Controller
    {
        private readonly AppDBContext _db;
        private string controller_name => "Roster";
        private string title_name => "Roster Karyawan";

        public RosterController(AppDBContext context)
        {
            _db = context;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("is_login") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var kategori_user_id = HttpContext.Session.GetString("kategori_user_id");

                var cek_kategori_user_id = _db.tbl_r_menu
                    .Where(x => x.link_controller == controller_name)
                    .Where(x => x.kategori_user_id == kategori_user_id)
                    .Count();

                if (cek_kategori_user_id > 0)
                {
                    ViewBag.Title = title_name;
                    ViewBag.Controller = controller_name;
                    ViewBag.Setting = _db.tbl_m_setting_aplikasi.FirstOrDefault();
                    ViewBag.Menu = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                    .OrderBy(x => x.title)
                        .ToList();
                    ViewBag.MenuPelanggaranCount = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Pelanggaran")
                    .OrderBy(x => x.title)
                        .Count();
                    ViewBag.MenuMasterCount = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Master")
                    .OrderBy(x => x.title)
                        .Count();
                    ViewBag.MenuTransaksiCount = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Transaksi")
                        .OrderBy(x => x.title)
                        .Count();
                    ViewBag.insert_by = HttpContext.Session.GetString("nrp");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        public async Task<IActionResult> GetList()
        {
            try
            {
                string[] ids = new string[] { "10011720001", "21071710340", "22051850596" };
                var results = _db.vw_roster_karyawan.Where(x => ids.Contains(x.nik)).ToList();
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

            if (HttpContext.Session.GetString("is_login") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var kategori_user_id = HttpContext.Session.GetString("kategori_user_id");

                var cek_kategori_user_id = _db.tbl_r_menu
                    .Where(x => x.link_controller == controller_name)
                    .Where(x => x.kategori_user_id == kategori_user_id)
                    .Count();

                if (cek_kategori_user_id > 0)
                {
                    ViewBag.Title = title_name;
                    ViewBag.Controller = controller_name;
                    ViewBag.Setting = _db.tbl_m_setting_aplikasi.FirstOrDefault();
                    ViewBag.Menu = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                    .OrderBy(x => x.title)
                        .ToList();
                    ViewBag.MenuPelanggaranCount = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Pelanggaran")
                    .OrderBy(x => x.title)
                        .Count();
                    ViewBag.MenuMasterCount = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Master")
                    .OrderBy(x => x.title)
                        .Count();
                    ViewBag.MenuTransaksiCount = _db.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Transaksi")
                        .OrderBy(x => x.title)
                        .Count();
                    ViewBag.insert_by = HttpContext.Session.GetString("nrp");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        [HttpGet("GetRosterByNik/{id}")]
        public async Task<IActionResult> GetRosterByNik(string id)
        {
            return Ok();
        }
    }
}
