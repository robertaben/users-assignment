using BlazorUserApp.Client;
using BlazorUserApp.Client.HttpClients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7174/") });
builder.Services.AddScoped<UsersHttpClient>();

await builder.Build().RunAsync();
