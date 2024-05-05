using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Context;

namespace ShiftPlan.Api.Extensions;

public static class AddEntityFrameworkExtension
{
	public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration config)
	{
		var connectionString = config.GetConnectionString("PostgresqlConnection");
		services.AddDbContext<ShiftPlanContext>(options => options.UseNpgsql(connectionString));
		return services;
	}
}
