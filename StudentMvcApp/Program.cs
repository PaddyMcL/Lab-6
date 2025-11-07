using Microsoft.EntityFrameworkCore;
using StudentMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Ensure the database path exists and is writable
builder.Services.AddDbContext<StudentMvcDbContext>(options =>
    options.UseSqlite("Data Source=C:\\Temp\\studentmvc.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
