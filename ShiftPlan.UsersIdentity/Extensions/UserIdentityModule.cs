using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ShiftPlan.UsersIdentity.Context;
using ShiftPlan.UsersIdentity.Models;
using ShiftPlan.UsersIdentity.Users;

namespace ShiftPlan.UsersIdentity.Extensions;

public static class UserIdentityModule
{
	public static IServiceCollection AddUserIdentity(this IServiceCollection services, string connectionString)
	{
		services.AddTransient<IUserContextService, UserContextService>();
		services.AddHttpContextAccessor();
		services.AddDbContext<IdentityUserContext>(options => options.UseNpgsql(connectionString));

		services.AddAuthorization();
		services.AddIdentityApiEndpoints<User>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<IdentityUserContext>();

		return services;

	}

	public static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			var securitySchemaId = "bearerAuth";
			options.AddSecurityDefinition(securitySchemaId, new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = securitySchemaId}
					},
					[]
				}
			});
		});

		return services;
	}
}
