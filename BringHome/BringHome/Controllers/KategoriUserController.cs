using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BringHome.Controllers
{
    public class KategoriUserController : Controller
    {
        private  AppDBContext _context;
        private  string controller_name => "KategoriUser";
        private  string title_name => "KategoriUser";

        public KategoriUserController(AppDBContext context)
        {
            _context = context;
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

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await _context.tbl_r_kategori_user.OrderBy(x => x.kategori).ToListAsync();
                return Json(new { status = true, remarks = "Sukses", data = results });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        // GET: KategoriUser/Get/5
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _context.tbl_r_kategori_user.FirstOrDefaultAsync(x => x.id == id);
                if (result == null)
                {
                    return Json(new { status = false, remarks = "Kategori tidak ditemukan" });
                }

                return Json(new { status = true, remarks = "Sukses", data = result });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        // POST: KategoriUser/Insert
        [HttpPost]
        public async Task<IActionResult> Insert(tbl_r_kategori_user a)
        {
            try
            {
                a.ip = System.Environment.MachineName;
                //a.created_at = DateTime.Now;
                _context.tbl_r_kategori_user.Add(a);
                await _context.SaveChangesAsync();
                return Json(new { status = true, remarks = "Sukses" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        // POST: KategoriUser/Update
        [HttpPost]
        public async Task<IActionResult> Update(tbl_r_kategori_user a)
        {
            try
            {
                var existingRecord = await _context.tbl_r_kategori_user.FirstOrDefaultAsync(f => f.id == a.id);
                if (existingRecord != null)
                {
                    existingRecord.kategori = a.kategori;
                    existingRecord.login_controller = a.login_controller;
                    existingRecord.login_function = a.login_function;
                    existingRecord.insert_by = a.insert_by;
                    existingRecord.ip = System.Environment.MachineName;
                   // existingRecord.updated_at = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return Json(new { status = true, remarks = "Sukses" });
                }
                else
                {
                    return Json(new { status = false, remarks = "Data tidak ditemukan" });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        // POST: KategoriUser/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingRecord = await _context.tbl_r_kategori_user.FirstOrDefaultAsync(f => f.id == id);
                if (existingRecord != null)
                {
                    _context.tbl_r_kategori_user.Remove(existingRecord);
                    await _context.SaveChangesAsync();
                    return Json(new { status = true, remarks = "Sukses" });
                }
                else
                {
                    return Json(new { status = false, remarks = "Data tidak ditemukan" });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }
    }
}
