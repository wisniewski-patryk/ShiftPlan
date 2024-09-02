using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShiftPlan.Blazor.Commons.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.RegisterServices(builder.Configuration);

await builder.Build().RunAsync();
