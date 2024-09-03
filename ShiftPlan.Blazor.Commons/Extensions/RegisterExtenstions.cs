using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShiftPlan.Blazor.Commons.Extensions;

public static class RegisterExtenstions
{
	public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.RegisterExternalLibrary();
		services.RegisterAddTokenMiddleware();

		var offlineMode = configuration.GetSection("OfflineMode").Value;
		if (bool.TryParse(offlineMode, out bool isOfflineModeEnabled))
		{
			services.RegisterOfflineServices();
			return services;
		}

		var backendApiUrl = new Uri(configuration.GetConnectionString("ShiftPlanBackendApiUrl") ?? throw new Exception("Backend api url not available."));
		services.RegisterHttpClient(backendApiUrl);

		services.RegisterApplicationServices();
		return services;
	}
}
