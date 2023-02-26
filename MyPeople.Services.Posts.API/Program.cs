using Microsoft.IdentityModel.Logging;
using MyPeople.Services.Posts.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOpenIddict();
builder.Services.ConfigureAuthentication();
builder.Services.AddAuthorization();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
