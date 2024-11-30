using ShiftPlan.Blazor.WebAssembly.Models;
using ShiftPlan.Blazor.WebAssembly.Services;
using ShiftPlan.Blazor.WebAssembly.Services.OfflineServices;

namespace ShiftPlan.Blazor.WebAssembly;

public static class RegistrationExtenstion
{
	public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		var offlineMode = configuration.GetSection("OfflineMode").Value;
		bool.TryParse(offlineMode, out var isOfflineModeEnabled);
		if (isOfflineModeEnabled)
		{
			services.RegisterOfflineServices();
			return services;
		}


		services.RegisterApplicationServices();
		return services;
	}

	public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<IEmployeesService, EmployeesService>();
		services.AddTransient<IShiftsService, ShiftsService>();
		return services;
	}

	public static IServiceCollection RegisterToastMessagesList(this IServiceCollection services)
	{
		return services;
	}

	public static IServiceCollection RegisterOfflineServices(this IServiceCollection services)
	{
		services.AddSingleton<List<Shift>>();
		services.AddSingleton<List<Employee>>();

		services.AddTransient<IEmployeesService, OfflineEmploeeyServices>();
		services.AddTransient<IShiftsService, OfflineShiftsService>();

		return services;
	}
}
