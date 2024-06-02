using indeapps.Models;
using Microsoft.EntityFrameworkCore;

namespace indeapps.DBContext
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<TbLUserLogin> tbl_m_login { get; set; }
        public DbSet<TbLUserLogin> tbl_r_kategori_user { get; set; }
        public DbSet<TbLUserLogin> tbl_m_setting_aplikasi { get; set; }
    }
}
