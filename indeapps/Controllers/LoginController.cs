using indeapps.DBContext;
using indeapps.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace indeapps.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDBContext _dbcontext;

        public LoginController(AppDBContext context)
        {
            _dbcontext = context;
        }
        // GET: Login
        //public ActionResult Index()
        //{
        //    if (Session["is_login"] != null)
        //    {
        //        var kategori_user_id = Session["kategori_user_id"];
        //        var cek_kategori_user_id = db.tbl_r_kategori_users.Where(x => x.kategori == kategori_user_id).FirstOrDefault();

        //        return RedirectToAction(cek_kategori_user_id.login_function, cek_kategori_user_id.login_controller);
        //    }
        //    else
        //    {
        //        ViewBag.Setting = db.tbl_m_setting_aplikasis.FirstOrDefault();
        //        return View();
        //    }
        //}

        //url: /Login/Go
        [HttpPost]
        public async Task<IActionResult> Go([FromBody] TbLUserLogin param)
        {
            var user = _dbcontext.tbl_m_login.Where(x => x.NRP == param.NRP && x.Password == param.Password).SingleOrDefault();

            if (user == null) { 
                return NotFound(new {Status = "failed"});
            } else
            {
                return Ok(new { Status = "success" });
            }
        }
    }
}
