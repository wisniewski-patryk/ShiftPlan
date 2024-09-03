using Microsoft.Extensions.DependencyInjection;
using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;
using ShiftPlan.Blazor.Commons.Services.OfflineServices;

namespace ShiftPlan.Blazor.Commons.Extensions;

public static class OfflineServicesRegistrationExtension
{
	public static IServiceCollection RegisterOfflineServices(this IServiceCollection services)
	{
		services.AddSingleton<List<Shift>>();
		services.AddSingleton<List<Employee>>();

		services.AddTransient<IEmployeesService, OfflineEmploeeyServices>();
		services.AddTransient<IShiftsService, OfflineShiftsService>();
		services.AddSingleton<IUserIdentityService, OfflineUserIdentityService>();

		return services;
	}
}
