using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BringHome.DBContext;
using BringHome.Models;

namespace BringHome.Controllers
{
    public class DepartemenController : Controller
    {
        private readonly AppDBContext _context;
        private readonly string controller_name = "Departemen";
        private readonly string title_name = "Departemen";

        public DepartemenController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Departemen
        public async Task<IActionResult> Index()
        {
            try
            {
                var departemens = await _context.tbl_r_dept.ToListAsync();
                return View(departemens);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Kesalahan internal server: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(tbl_r_dept a)
        {
            try
            {
                // Mengisi properti ip dengan nama mesin
                a.ip = System.Environment.MachineName;
                //  a.created_at = DateTime.Now;

                // Menambahkan data ke konteks dan menyimpan perubahan
                _context.tbl_r_dept.Add(a);
                await _context.SaveChangesAsync();

                // Mengembalikan respons sukses
                return Json(new { status = true, remarks = "Sukses" });
            }
            catch (Exception e)
            {
                // Mengembalikan respons gagal beserta pesan kesalahan
                return Json(new { status = false, remarks = "Gagal", data = e.Message });
            }
        }

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await _context.tbl_r_dept.OrderBy(x => x.created_at).ToListAsync();
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
                var result = await _context.tbl_r_dept.FirstOrDefaultAsync(x => x.id == id);
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
        public async Task<IActionResult> Update(tbl_r_dept a)
        {
            try
            {
                var tbl_ = await _context.tbl_r_dept.FirstOrDefaultAsync(f => f.id == a.id);
                if (tbl_ != null)
                {
                    tbl_.dept_code = a.dept_code;
                    tbl_.departemen = a.departemen;
                    tbl_.insert_by = a.insert_by;
                    tbl_.ip = System.Environment.MachineName;
                    //tbl_.updated_at = DateTime.Now;

                    _context.tbl_r_dept.Update(tbl_);
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
                var tbl_ = await _context.tbl_r_dept.FirstOrDefaultAsync(f => f.id == id);
                if (tbl_ != null)
                {
                    _context.tbl_r_dept.Remove(tbl_);
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
