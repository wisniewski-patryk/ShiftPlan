using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Context;

namespace ShiftPlan.Api.Extensions;

public static class AddEntityFrameworkExtension
{
	public static IServiceCollection AddEntityFramework(this IServiceCollection services)
	{
		services.AddDbContext<ShiftPlanContext>(options => options.UseNpgsql("Server=localhost:5432;Database=shiftplandb;Username=shiftplan;Password=dev_password"));
		return services;
	}
}
