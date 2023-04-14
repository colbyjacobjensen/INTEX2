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
using Microsoft.ML.OnnxRuntime;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // Remove the default logging provider

builder.Logging.AddConsole(); // Add the console logging provider

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BuffaloDBConnection");

builder.Services.AddDbContext<BuffaloDbContext>(options =>
	options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<BuffaloDbContext>();

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

// app.UseHttpsRedirection(); // Remove this line to disable HTTPS redirection

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.MapControllerRoute(
	name: "typepage",
	pattern: "{burialType}/Page{pageNum}",
	defaults: new { Controller = "Home", action = "Index" });

app.MapControllerRoute(
	name: "Paging",
	pattern: "Page{pageNum}",
	defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

app.MapControllerRoute(
	name: "type",
	pattern: "{burialType}",
	defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

app.MapDefaultControllerRoute(); // Use default pattern to send user to "Index"

app.MapRazorPages();

app.Run();
