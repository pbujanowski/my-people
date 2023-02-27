using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyPeople.Client;
using MyPeople.Client.Services;
using MyPeople.Services.Posts.Application.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddOidcAuthentication(options => builder.Configuration.Bind("Oidc", options.ProviderOptions));
builder.Services.AddHttpClient("services.posts", cl => cl.BaseAddress = new Uri("http://localhost:5000/"))
    .AddHttpMessageHandler(sp =>
        sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { "http://localhost:5000" },
            scopes: new[] { "services.posts" }
        )
    );
builder.Services.AddScoped<IPostService>(sp =>
    new PostService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("services.posts")));

await builder.Build().RunAsync();