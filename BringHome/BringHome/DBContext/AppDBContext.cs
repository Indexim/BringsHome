using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BringHome.Models;

namespace BringHome.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<tbl_m_user_login> tbl_m_user_login { get; set; }
        public DbSet<tbl_r_kategori_user> tbl_r_kategori_user { get; set; }
        public DbSet<tbl_m_setting_aplikasi> tbl_m_setting_aplikasi { get; set; }
        public DbSet<Roster> roster_karyawan { get; set; }
        public DbSet<vw_t_user_kategori> vw_t_user_kategori { get; set; }
        public DbSet<tbl_r_dept> tbl_r_dept { get; set; } // Added DbSet for tbl_r_dept

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurasi entitas yang tidak memiliki kunci utama (keyless entity)
            modelBuilder.Entity<vw_t_user_kategori>().HasNoKey();

            // Optionally configure tbl_r_dept if needed
            // Example: Configure table name and primary key
            modelBuilder.Entity<tbl_r_dept>(entity =>
            {
                entity.ToTable("tbl_r_dept"); // Optional: Specify table name if different
                entity.HasKey(e => e.id); // Optional: Specify primary key if needed
            });
        }
    }
}
