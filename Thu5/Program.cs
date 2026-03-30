using Thu5.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
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

app.UseSession(); // ⚠️ BẮT BUỘC nếu dùng login

app.UseAuthorization();

// ====== ROUTE ======
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
);

// ====== SEED DATA (tài xế) ======
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Drivers.Any())
    {
        db.Drivers.Add(new Thu5.Models.Driver { Name = "Tài xế A", IsOnline = true });
        db.Drivers.Add(new Thu5.Models.Driver { Name = "Tài xế B", IsOnline = true });
        db.SaveChanges();
    }
}

app.Run();