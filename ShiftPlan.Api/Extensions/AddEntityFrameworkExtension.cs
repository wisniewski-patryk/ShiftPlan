using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Context;

namespace ShiftPlan.Api.Extensions;

public static class AddEntityFrameworkExtension
{
	public static IServiceCollection AddEntityFramework(this IServiceCollection services, string connectionString)
	{
		
		services.AddDbContext<ShiftPlanContext>(options => options.UseNpgsql(connectionString));
		return services;
	}
}
