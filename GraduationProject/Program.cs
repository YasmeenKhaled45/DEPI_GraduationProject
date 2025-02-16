using GraduationProject.Data;
using GraduationProject.filter;
using GraduationProject.Interface;
using GraduationProject.Repositories;
using GraduationProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// تسجيل الفلتر كجزء من النظام (اختياري إذا أردت إضافة فلتر على مستوى التطبيق)
builder.Services.AddMvc(options =>
{
    // إضافة فلتر يتم تطبيقه على جميع الأكشنز افتراضيًا
    options.Filters.Add<Logginfilter>();
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Users/Login"; // Your custom login page
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // تأكد من إضافة هذه السطر لتفعيل المصادقة
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
