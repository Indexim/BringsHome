﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BringHome.DBContext;
using BringHome.Models;

namespace BringHome.Controllers
{
    public class KaryawanController : Controller

    {

        private AppDBContext _context;
        private string controller_name => "Karyawan";
        private string title_name => "Karyawan";
        public KaryawanController(AppDBContext context)
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
    }
}
