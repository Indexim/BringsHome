﻿using BringHome.DBContext;
using BringHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BringHome.Controllers
{
    public class MenuController : Controller
    {
        private AppDBContext _context;
        private string controller_name => "Menu";
        private string title_name => "Menu";

        public MenuController(AppDBContext context)
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

                    // Menambahkan link action ke ViewBag
                    ViewBag.FunctionLink = Url.Action("Function", "Controller");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }



        // GET: Menu/GetAll
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await _context.tbl_r_menu.OrderBy(x => x.type).ToListAsync();
                return Json(new { status = true, remarks = "Sukses", data = results });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }


        // GET: Menu/Get/{id}
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _context.tbl_r_menu.FirstOrDefaultAsync(x => x.id == id);
                if (result == null)
                {
                    return Json(new { status = false, remarks = "Menu tidak ditemukan" });
                }

                return Json(new { status = true, remarks = "Sukses", data = result });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        // POST: Menu/Insert
        [HttpPost]
        public async Task<IActionResult> Insert(tbl_r_menu a)
        {
            try
            {
                a.ip = System.Environment.MachineName;
                //  a.created_at = DateTime.Now;

                _context.tbl_r_menu.Add(a);
                await _context.SaveChangesAsync();

                return Json(new { status = true, remarks = "Sukses" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        // POST: Menu/Update
        [HttpPost]
        public async Task<IActionResult> Update(tbl_r_menu a)
        {
            try
            {
                var tbl_ = await _context.tbl_r_menu.FirstOrDefaultAsync(x => x.id == a.id);
                if (tbl_ != null)
                {
                    tbl_.kategori_user_id = a.kategori_user_id;
                    tbl_.type = a.type;
                    tbl_.title = a.title;
                    tbl_.link_controller = a.link_controller;
                    tbl_.link_function = a.link_function;
                    tbl_.hidden = a.hidden;
                    tbl_.new_tab = a.new_tab;
                    tbl_.insert_by = a.insert_by;
                    tbl_.ip = System.Environment.MachineName;
                    //  tbl_.updated_at = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return Json(new { status = true, remarks = "Sukses" });
                }

                return Json(new { status = false, remarks = "Menu tidak ditemukan" });
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
                var tbl_ = await _context.tbl_r_menu.FirstOrDefaultAsync(x => x.id == id);
                if (tbl_ != null)
                {
                    _context.tbl_r_menu.Remove(tbl_);
                    await _context.SaveChangesAsync();

                    return Json(new { status = true, remarks = "Sukses" });
                }

                return Json(new { status = false, remarks = "Menu tidak ditemukan" });
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }
    }
}