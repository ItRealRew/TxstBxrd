using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using TXSTBXRD_UI;
using TXSTBXRD_UI.Utils;
using TXSTBXRD_UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ILocalStorage, LocalStorage>();
builder.Services.AddScoped<ICookie, Cookie>();

builder.Services.AddHttpClient<IdentityService>("IdentityAPI", (sharepoint, client) =>
    {
        client.BaseAddress = new Uri("https://localhost:5001/identity/");
        client.EnableIntercept(sharepoint);
    });

 builder.Services.AddScoped(
        sharepoint => sharepoint.GetService<IHttpClientFactory>().CreateClient("IdentityAPI"));

builder.Services.AddHttpClientInterceptor();

builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();

