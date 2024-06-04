using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BringHome.DBContext;
using BringHome.Models;
namespace BringHome.Controllers
{
    public class UserLoginController : Controller
    {
        private  AppDBContext _context;
        private  string controller_name => "UserLogin";
        private  string title_name => "UserLogin";
        public UserLoginController(AppDBContext context)
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
                var results = await _context.tbl_m_user_login.OrderBy(x => x.nrp).ToListAsync();
                return Json(new { status = true, remarks = "Sukses", data = results });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _context.tbl_m_user_login.FirstOrDefaultAsync(x => x.id == id);
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
        public async Task<IActionResult> Insert(tbl_m_user_login a)
        {
            try
            {
                a.ip = System.Environment.MachineName;
             //   a.created_at = DateTime.Now;

                _context.tbl_m_user_login.Add(a);
                await _context.SaveChangesAsync();

                return Json(new { status = true, remarks = "Sukses" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(tbl_m_user_login a)
        {
            try
            {
                var tbl_ = await _context.tbl_m_user_login.FirstOrDefaultAsync(x => x.id == a.id);
                if (tbl_ != null)
                {
                    tbl_.kategori_user_id = a.kategori_user_id;
                    tbl_.nrp = a.nrp;
                    tbl_.password = a.password;
                    tbl_.fullname = a.fullname;
                    tbl_.dept_code = a.dept_code;
                    tbl_.created_by = a.created_by;
                    tbl_.ip = System.Environment.MachineName;
                   // tbl_.updated_at = DateTime.Now;

                    _context.tbl_m_user_login.Update(tbl_);
                    await _context.SaveChangesAsync();

                    return Json(new { status = true, remarks = "Sukses" });
                }
                return Json(new { status = false, remarks = "Data tidak ditemukan" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tbl_ = await _context.tbl_m_user_login.FirstOrDefaultAsync(f => f.id == id);
                if (tbl_ != null)
                {
                    _context.tbl_m_user_login.Remove(tbl_);
                    await _context.SaveChangesAsync();

                    return Json(new { status = true, remarks = "Sukses" });
                }
                return Json(new { status = false, remarks = "Data tidak ditemukan" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }
    }
}