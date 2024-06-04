using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BringHome.Controllers
{
    public class SettingController : Controller
    {
        private  AppDBContext _context;
        private  string controller_name => "Setting";
        private  string title_name => "Setting";

        public SettingController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Departemen
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("is_login") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var kategori_user_id = HttpContext.Session.GetString("kategori_user_id");

                var cek_kategori_user_id = _context.tbl_r_menu
                    .Where(x => x.link_controller == controller_name)
                    .Where(x => x.kategori_user_id == kategori_user_id)
                    .Count();

                if (cek_kategori_user_id > 0)
                {
                    ViewBag.Title = title_name;
                    ViewBag.Controller = controller_name;
                    ViewBag.Setting = _context.tbl_m_setting_aplikasi.FirstOrDefault();
                    ViewBag.Menu = _context.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .OrderBy(x => x.title)
                        .ToList();
                    ViewBag.MenuPelanggaranCount = _context.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Pelanggaran")
                        .OrderBy(x => x.title)
                        .Count();
                    ViewBag.MenuMasterCount = _context.tbl_r_menu
                        .Where(x => x.kategori_user_id == HttpContext.Session.GetString("kategori_user_id"))
                        .Where(x => x.type == "Master")
                        .OrderBy(x => x.title)
                        .Count();
                    ViewBag.MenuTransaksiCount = _context.tbl_r_menu
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _context.tbl_m_setting_aplikasi.FirstOrDefaultAsync();
                if (result == null)
                {
                    return Json(new { status = false, remarks = "Data tidak ditemukan" });
                }
                return Json(new { status = true, remarks = "Sukses", data = result });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(tbl_m_setting_aplikasi a)
        {
            try
            {
                var tbl_ = await _context.tbl_m_setting_aplikasi.FirstOrDefaultAsync(f => f.id == a.id);
                if (tbl_ != null)
                {
                    tbl_.id = a.id;
                    tbl_.nama = a.nama;
                    tbl_.description = a.description;
                    tbl_.icon = a.icon;
                    tbl_.theme = a.theme;
                    tbl_.insert_by = a.insert_by;
                    tbl_.ip = System.Environment.MachineName; // Menetapkan nilai ip dari objek a
                  //  tbl_.updated_at = DateTime.Now; // Menetapkan nilai updated_at dari objek a
                    await _context.SaveChangesAsync(); // Menyimpan perubahan ke database
                }
                return Json(new { status = true, remarks = "Sukses" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }


    }
}