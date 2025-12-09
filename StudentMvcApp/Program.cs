using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentMvcApp.Data;
using StudentMvcApp.Models;
using StudentMvcApp.Seed;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// 1. Add MVC + Razor Pages
// ----------------------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Required for Identity pages

// ----------------------
// 2. Lab 6 Database Context
// ----------------------
builder.Services.AddDbContext<StudentMvcDbContext>(options =>
    options.UseSqlite("Data Source=C:\\Temp\\studentmvc.db"));

// ----------------------
// 3. Identity Database Context
// ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=C:\\Temp\\student_identity.db"));

// ----------------------
// 4. Identity + Roles Setup
// ----------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ----------------------
// 5. Build App
// ----------------------
var app = builder.Build();

// ----------------------
// 6. Apply migrations + seed roles/admin
// ----------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Apply Identity migrations
    var identityDb = services.GetRequiredService<ApplicationDbContext>();
    identityDb.Database.Migrate();

    // Apply Student MVC DB migrations
    var studentDb = services.GetRequiredService<StudentMvcDbContext>();
    studentDb.Database.Migrate();

    // Seed roles and admin
    await IdentitySeeder.SeedAsync(roleManager, userManager);
}

// ----------------------
// 7. Middleware
// ----------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ----------------------
// 8. Endpoints
// ----------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Required for Identity pages like /Identity/Account/Register

// ----------------------
// 9. Run
// ----------------------
app.Run();
