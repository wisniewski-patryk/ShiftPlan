using BlazorBootstrap;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using ShiftPlan.Blazor.Commons.Clients;
using ShiftPlan.Blazor.Commons.Middlewares;
using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Commons.Extensions;

public static class ServiceRegistrationExtensions
{
	public static IServiceCollection RegisterExternalLibrary(this IServiceCollection services)
	{
		services.AddBlazorBootstrap();
		services.AddBlazoredSessionStorage();
		return services;
	}

	public static IServiceCollection RegisterHttpClient(this IServiceCollection services, Uri backendApiUrl)
	{
		services.AddHttpClient<IEmployeesClient, EmployeesClient>(client => client.BaseAddress = backendApiUrl)
			.AddHttpMessageHandler<HttpSendRequestMiddleware>();
		services.AddHttpClient<IShiftsClient, ShiftsClient>(client => client.BaseAddress = backendApiUrl)
			.AddHttpMessageHandler<HttpSendRequestMiddleware>();
		services.AddHttpClient<IUserIdentityClient, UserIdentityClient>(client => client.BaseAddress = backendApiUrl);
		return services;
	}

	public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<IEmployeesService, EmployeesService>();
		services.AddTransient<IShiftsService, ShiftsService>();
		services.AddTransient<IUserIdentityService, UserIdentityService>();
		return services;
	}

	public static IServiceCollection RegisterToastMessagesList(this IServiceCollection services)
	{
		services.AddSingleton<List<ToastMessage>>();
		return services;
	}

}
