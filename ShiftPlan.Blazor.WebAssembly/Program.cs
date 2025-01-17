using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShiftPlan.Blazor.Client.Identity;
using ShiftPlan.Blazor.WebAssembly;
using ShiftPlan.Blazor.WebAssembly.Clients;
using ShiftPlan.Blazor.WebAssembly.Identity;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register the cookie handler
builder.Services.AddTransient<CookieHandler>();

// set up authorization
builder.Services.AddAuthorizationCore();

// register the custom state provider
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();


// register the account management interface
builder.Services.AddScoped(
	sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddBlazorBootstrap();

builder.Services.AddTransient<IEmployeesClient, EmployeesClient>();
builder.Services.AddTransient<IShiftsClient, ShiftsClient>();
builder.Services.AddTransient<IRolesClient, RolesClient>();
builder.Services.AddTransient<UserManagmentClient>();
builder.Services.RegisterServices(builder.Configuration);

var apiUrl = builder.Configuration.GetConnectionString("ShiftplanApi") ?? throw new Exception("Url to api is unavaiable");

// configure client for auth interactions
builder.Services.AddHttpClient(
	"Auth",
	opt => opt.BaseAddress = new Uri(apiUrl))
	.AddHttpMessageHandler<CookieHandler>();

// set base address for default host
builder.Services.AddScoped(sp =>
	(HttpClient)sp.GetRequiredService<IHttpClientFactory>().CreateClient("Auth"));

await builder.Build().RunAsync();
