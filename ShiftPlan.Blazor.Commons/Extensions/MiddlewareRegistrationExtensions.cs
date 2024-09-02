using Microsoft.Extensions.DependencyInjection;
using ShiftPlan.Blazor.Commons.Middlewares;

namespace ShiftPlan.Blazor.Commons.Extensions;

public static class MiddlewareRegistrationExtensions
{
	public static IServiceCollection RegisterAddTokenMiddleware(this IServiceCollection services)
	{
		services.AddTransient<HttpSendRequestMiddleware>();
		return services;
	}
}
