using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BringHome.Controllers
{
    public class LoginController : Controller
    {
        private  AppDBContext _db;

        public LoginController(AppDBContext context)
        {
            _db = context;
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
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
        public ActionResult ProsesLogin(string nrp, string password)
        {
            try
            {
                // Cek keberadaan NRP di tbl user login
                var userLogin = _db.tbl_m_user_login.FirstOrDefault(x => x.nrp == nrp && x.password == password);

                if (userLogin != null)
                {
                    HttpContext.Session.SetString("is_login", "true");
                    HttpContext.Session.SetString("nrp", userLogin.nrp);
                    HttpContext.Session.SetString("nama", userLogin.fullname);
                    HttpContext.Session.SetString("dept", userLogin.dept_code);


                    var kategoriUser = _db.tbl_m_user_login.Where(x => x.nrp == userLogin.nrp).ToList();

                    return Json(new { status = true, remarks = "Login Sukses: " + userLogin.kategori_user_id, data = kategoriUser });
                }
                else
                {
                    return Json(new { status = false, remarks = "Login gagal. Username/password salah" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult CekKategoriUser(string kategori_user_id)
        {
            try
            {
                var result = _db.tbl_r_kategori_user.FirstOrDefault(x => x.kategori == kategori_user_id);
                HttpContext.Session.SetString("kategori_user_id", result.kategori);
                return Json(new { status = true, remarks = "Sukses", data = result });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message });
            }
        }
    }
}
