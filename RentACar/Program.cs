using Microsoft.EntityFrameworkCore;
using RentACar.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using RentACar.Utility;
using Microsoft.Extensions.Options;
using RentACar.Core.Services;
using RentACar.Core.IServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RentACar.DataAccess.IRepository;
using RentACar.DataAccess.IRepository.Repository;
using RentACar.Models;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RentACarDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("RentACar.DataAccess")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<RentACarDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<ICarCompanyService, CarCompanyService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IClassOfCarService, ClassOfCarService>();
builder.Services.AddScoped<ICTypeService, CTypeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ICarFeatureService, CarFeatureService>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CompanyPolicy", policy => policy.RequireRole("Company"));
});
var cloudinarySettings = builder.Configuration

                         .GetSection("Cloudinary")

                         .Get<CloudinarySettings>();



var account = new Account(cloudinarySettings.CloudName,

cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);



var cloudinary = new Cloudinary(account);

builder.Services.AddSingleton(cloudinary);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateAdmin(services);
}

app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
 static async Task CreateRoles(IServiceProvider serviceProvider)

{

    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roleNames = { SD.Admin, SD.User, SD.Company };



    foreach (var roleName in roleNames)

    {

        if (!await roleManager.RoleExistsAsync(roleName))

        {

            await roleManager.CreateAsync(new IdentityRole(roleName));

        }

    }

}
 static async Task CreateAdmin(IServiceProvider serviceProvider)

{

    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var adminEmail = "admin@admin.com";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);





    if (adminUser == null)

    {

        var user = new IdentityUser { UserName = "admin@admin.com", Email = adminEmail };

        var result = await userManager.CreateAsync(user, "AdminPassword123!");

        if (result.Succeeded)

        {

            await userManager.AddToRoleAsync(user, SD.Admin); 

        }

    }

}

