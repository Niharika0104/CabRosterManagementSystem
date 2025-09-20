using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ZelisCabPlatform;
using ZelisCabPlatform.Services;
using Blazored.SessionStorage;
using Blazored.Toast;
using ZelisCabPlatform.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7185/api/") });
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<CabService>();
builder.Services.AddScoped<ManagerService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
await builder.Build().RunAsync();
