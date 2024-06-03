using Microsoft.EntityFrameworkCore;
using BringHome.Models;
using BringHome;
using BringHome.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tambahkan layanan sesi
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Tambahkan middleware sesi ke dalam pipeline
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Karyawan}/{action=Index}/{id?}");

app.Run();
