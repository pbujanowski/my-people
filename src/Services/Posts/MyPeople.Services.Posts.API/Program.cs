using Microsoft.IdentityModel.Logging;
using MyPeople.Common.Logging;
using MyPeople.Common.Logging.Extensions;
using MyPeople.Services.Common.Extensions;
using MyPeople.Services.Posts.Application;
using MyPeople.Services.Posts.Infrastructure;

LoggingInitializer.Initialize(async () =>
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.ConfigureLogging();
    builder.Services.ConfigureApplication();
    builder.Services.ConfigureInfrastructure(builder.Configuration);
    builder.Services.ConfigureCors(builder.Configuration);
    builder.Services.ConfigureOpenIddict(builder.Configuration);
    builder.Services.ConfigureAuthentication();

    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        IdentityModelEventSource.ShowPII = true;
    }

    await app.UseInfrastructureAsync();

    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/healthcheck");

    app.Run();
});
