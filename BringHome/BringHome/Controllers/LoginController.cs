using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BringHome.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDBContext _db;

        public LoginController(AppDBContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("is_login") != null)
            {
                var kategori_user_id = HttpContext.Session.GetString("kategori_user_id");
                var cek_kategori_user_id = _db.tbl_r_kategori_user.FirstOrDefault(x => x.kategori == kategori_user_id);

                return RedirectToAction(cek_kategori_user_id.login_function, cek_kategori_user_id.login_controller);
            }
            else
            {
                ViewBag.Setting = _db.tbl_m_setting_aplikasi.FirstOrDefault();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProsesLogin(string nrp, string password, string ip)
        {
            try
            {
                // Cari pengguna berdasarkan NRP dan kata sandi dari database
                var user = _db.tbl_m_user_login.FirstOrDefault(x => x.nrp == nrp && x.password == password);

                if (user != null)
                {
                    // Data pengguna ditemukan, set session dan kirimkan respons
                    HttpContext.Session.SetString("is_login", "true");
                    HttpContext.Session.SetString("nrp", user.nrp);
                    HttpContext.Session.SetString("nama", user.fullname);
                    HttpContext.Session.SetString("dept", user.dept_code);
                    HttpContext.Session.SetString("ip", ip);

                    // Ambil kategori pengguna dari database
                    var kategori_user = _db.vw_t_user_kategori.Where(x => x.nrp == user.nrp).ToList();

                    return Json(new { status = true, remarks = "Login Sukses: " + user.nrp, data = kategori_user });
                }
                else
                {
                    // Data pengguna tidak ditemukan, kirimkan pesan kesalahan
                    return Json(new { status = false, remarks = "Login gagal. Username/password salah" });
                }
            }
            catch (Exception ex)
            {
                // Tangani kesalahan jika terjadi
                return Json(new { status = false, remarks = "Gagal", data = ex.Message });
            }
        }



        [HttpGet]
        public IActionResult CekKategoriUser(string kategori_user_id)
        {
            try
            {
                var results = _db.tbl_r_kategori_user.FirstOrDefault(x => x.kategori == kategori_user_id);
                HttpContext.Session.SetString("kategori_user_id", results.kategori);
                return Json(new { status = true, remarks = "Sukses", data = results });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }
    }
}
