using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyPeople.Client;
using MyPeople.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureHttpClients();
builder.Services.ConfigureScoped();

await builder.Build().RunAsync();