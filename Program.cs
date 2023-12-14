using CompanionApp;
using CompanionApp.Services;
using CompanionApp.States;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Fast.Components.FluentUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IHoleCalculationService, HoleCalculationService>();
builder.Services.AddScoped<CalculationSettingsStateContainer>();
builder.Services.AddScoped<CalculationStateContainer>();
builder.Services.AddFluentUIComponents(options => { options.HostingModel = BlazorHostingModel.WebAssembly; });

await builder.Build().RunAsync();