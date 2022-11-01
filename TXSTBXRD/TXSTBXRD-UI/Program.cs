using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TXSTBXRD_UI;
using TXSTBXRD_UI.Utils;
using TXSTBXRD_UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<ICookie, Cookie>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<IdentityService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/identity/");
});

await builder.Build().RunAsync();

