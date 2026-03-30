using Thu5.Data;
using Thu5.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// ====== MVC ======
builder.Services.AddControllersWithViews();

// ====== DATABASE ======
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ====== SESSION ======

builder.Services.AddSession();

var app = builder.Build();

// ====== MIDDLEWARE ======
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // bắt buộc cho login

app.UseAuthorization();

// ====== ROUTE ======
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
);

// ====== SEED DATABASE ======
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // tạo DB nếu chưa có + apply migration
    db.Database.Migrate();

    // seed tài xế
    if (!db.Drivers.Any())
    {
        db.Drivers.Add(new Driver { Name = "Tài xế A", IsOnline = true });
        db.Drivers.Add(new Driver { Name = "Tài xế B", IsOnline = true });
        db.SaveChanges();
    }
}

app.Run();