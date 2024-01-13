using MyPeople.Gateways.Web.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors();

await app.UseOcelot();

app.Run();
