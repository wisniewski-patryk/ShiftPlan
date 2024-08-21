using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShiftPlan.Blazor.Commons.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.RegisterExternalLibrary();

var backendApiUrl = new Uri(builder.Configuration.GetConnectionString("ShiftPlanBackendApiUrl") ?? throw new Exception("Backend api url not available."));

builder.Services.RegisterHttpClient(backendApiUrl);
builder.Services.RegisterServices();

await builder.Build().RunAsync();
