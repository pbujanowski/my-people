using MyPeople.Common.Logging;
using MyPeople.Common.Logging.Extensions;
using MyPeople.Gateways.Web.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

LoggingInitializer.Initialize(
    async () =>
    {
        builder.Services.ConfigureLogging();
        builder.Services.ConfigureCors(builder.Configuration);

        var ocelotBuilder = builder.Services.AddOcelot();

        if (builder.Environment.IsStaging())
        {
            ocelotBuilder.AddPolly();
        }

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        await app.UseOcelot();

        app.Run();
    },
    builder.Configuration
);
