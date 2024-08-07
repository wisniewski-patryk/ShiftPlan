using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ShiftPlan.UsersIdentity;

public static class UserIdentityModule
{
	public static IServiceCollection AddUserIdentity(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<IdentityUserContext>(options => options.UseNpgsql(connectionString));
		services.AddAuthorization();
		services.AddIdentityApiEndpoints<IdentityUser>()
			.AddEntityFrameworkStores<IdentityUserContext>();
		return services;
	}
}
