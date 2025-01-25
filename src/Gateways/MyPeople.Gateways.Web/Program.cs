using MyPeople.Common.Configuration.Extensions;
using MyPeople.Common.Logging.Extensions;
using MyPeople.Gateways.Web.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureConfigurationProviders();

builder.Services.ConfigureLogging(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);

var ocelotBuilder = builder.Services.AddOcelot();

ocelotBuilder.AddPolly();

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
