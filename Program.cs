using INTEX2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using INTEX2.Areas.Identity.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using INTEX2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // Remove the default logging provider

builder.Logging.AddConsole(); // Add the console logging provider

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MummyDBConnection");

builder.Services.AddDbContext<mummydbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<IdentityContext>(opts =>
	opts.UseNpgsql(builder.Configuration[
		"ConnectionStrings:IdentityConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<IdentityOptions>(opts => {
    opts.Password.RequiredLength = 10;
    opts.Password.RequireNonAlphanumeric = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireDigit = true;
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<mummydbContext>();

builder.Services.AddScoped<IBurialRepository, EFBurialRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
	// This lambda determines whether user consent for non-essential 
	// cookies is needed for a given request.
	options.CheckConsentNeeded = context => true;

	options.MinimumSameSitePolicy = SameSiteMode.None;
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

//Use Static Files
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//MAY NEED TO UNCOMMENT
//app.UseEndpoints(endpoints =>
//{
	//endpoints.MapControllers();
//});

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; font-src 'self'; img-src 'self' cwadmin.byu.edu; frame-src 'self'");

    await next();
});

//Endpoints
app.MapControllerRoute(
    name: "typepage",
    pattern: "{burialType}/Page{pageNum}",
    defaults: new { Controller = "Home", action = "BurialList" });

app.MapControllerRoute(
    name: "Paging",
    pattern: "Page{pageNum}",
    defaults: new { Controller = "Home", action = "BurialList", pageNum = 1 });

app.MapControllerRoute(
    name: "type",
    pattern: "{burialType}",
    defaults: new { Controller = "Home", action = "BurialList", pageNum = 1 });

app.MapControllerRoute(
    name: "edit",
    pattern: "{controller=Home}/{action=Index}/{recordid?}");

app.MapDefaultControllerRoute(); // Use default pattern to send user to "Index"

app.MapRazorPages();

app.Run();
