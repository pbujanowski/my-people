using MyPeople.Common.Logging;
using MyPeople.Common.Logging.Extensions;
using MyPeople.Identity.Infrastructure;
using MyPeople.Identity.Web;
using MyPeople.Identity.Web.Extensions;

LoggingInitializer.Initialize(async () =>
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.ConfigureLogging();
    builder.Services.ConfigureInfrastructure(builder.Configuration);
    builder.Services.ConfigureCookiePolicy();
    builder.Services.ConfigureCors(builder.Configuration);
    builder.Services.ConfigureApplicationCookie();

    builder.Services.AddControllersWithViews();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    {
        builder.Services.AddHostedService<Worker>();
    }

    var app = builder.Build();

    if (!app.Environment.IsDevelopment() && !app.Environment.IsStaging())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    await app.UseInfrastructureAsync();

    app.UseStaticFiles();
    app.UseCookiePolicy();

    app.UseCors();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

    app.Run();
});
