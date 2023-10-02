using Microsoft.IdentityModel.Logging;
using MyPeople.Services.Images.API.Extensions;
using MyPeople.Services.Images.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureOpenIddict();
builder.Services.ConfigureAuthentication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.Run();
