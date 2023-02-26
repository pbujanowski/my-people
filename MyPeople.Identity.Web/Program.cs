using MyPeople.Identity.Web;
using MyPeople.Identity.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCookiePolicy();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureApplicationCookie();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureQuartz();
builder.Services.ConfigureOpenIddict();

builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHostedService<Worker>();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
