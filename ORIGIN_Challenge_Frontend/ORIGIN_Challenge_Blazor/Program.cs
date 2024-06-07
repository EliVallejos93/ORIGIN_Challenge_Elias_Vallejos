using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ORIGIN_Challenge_Blazor;
using ORIGIN_Challenge_Blazor.Services;
using ORIGIN_Challenge_Blazor.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para consumir la API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7112") });

// Configurar mis servicios
builder.Services.AddScoped<TarjetasService>();
builder.Services.AddScoped<OperacionesService>();
builder.Services.AddSingleton<AlertService>();

await builder.Build().RunAsync();