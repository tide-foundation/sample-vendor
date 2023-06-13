using Vendor.Helpers;
using Vendor.Services;
using Vendor_SDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(Vendor_SDK.Controllers.TideAuthController).Assembly); // add this

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10); // add this
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<VendorSDKOptions>(options =>
{
    options.RedirectUrl = "http://localhost:5231/home"; // add this
});


var services = builder.Services;
services.AddDbContext<DataContext>();
services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(builder => {
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

app.UseSession(); // add this


using (var scope = app.Services.CreateScope()) // Create table if does not exist
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.EnsureCreated();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


