using Microsoft.IdentityModel.Logging;
using MyPeople.Services.Common.Extensions;
using MyPeople.Services.Images.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureOpenIddict(builder.Configuration);
builder.Services.ConfigureAuthentication();

if (builder.Environment.IsStaging())
{
    builder.Services.ConfigureConsul(builder.Configuration);
}

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
