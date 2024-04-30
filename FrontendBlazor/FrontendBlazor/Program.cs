using FrontendBlazor.Client.Clients;
using FrontendBlazor.Client.Services;
using FrontendBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazorBootstrap();

var apiUrl = new Uri("https://localhost:5001/api/");

builder.Services.AddHttpClient<IEmployeesClient, EmployeesClient>(client => client.BaseAddress = apiUrl);

builder.Services.AddHttpClient<IShiftsClient, ShiftsClient>(client => client.BaseAddress = apiUrl);

builder.Services.AddTransient<IEmployeesService, EmployeesService>();
builder.Services.AddTransient<IShiftsService, ShiftsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(FrontendBlazor.Client._Imports).Assembly);

app.Run();
