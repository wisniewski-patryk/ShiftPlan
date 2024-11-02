using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Extensions;
using ShiftPlan.Blazor.Commons.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddTransient<ILoadSaveService, LocalFileService>();

await builder.Build().RunAsync();
