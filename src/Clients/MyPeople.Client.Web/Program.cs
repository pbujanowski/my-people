using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyPeople.Client.Infrastructure.Extensions;
using MyPeople.Client.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureHttpClients(builder.Configuration);
builder.Services.ConfigureScoped();

await builder.Build().RunAsync();
