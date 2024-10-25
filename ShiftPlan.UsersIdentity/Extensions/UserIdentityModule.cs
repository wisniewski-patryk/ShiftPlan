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
	private const string ROOT_ADMIN = "ROOT_ADMIN";
	private const string ROOT_ADMIN_EMAIL = "ROOT@ADMIN.EMAIL";

	public static IServiceCollection AddUserIdentity(this IServiceCollection services, string connectionString)
	{
		services.AddTransient<IUserContext, UserContext>();
		services.AddHttpContextAccessor();
		services.AddDbContext<IdentityUserContext>(options => options.UseNpgsql(connectionString));

		services.AddAuthorization();
		services.AddIdentityApiEndpoints<Models.User>()
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<IdentityUserContext>()
			.AddDefaultTokenProviders();

		services.CreateSuperRootUser();

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

	private static IServiceCollection CreateSuperRootUser(this IServiceCollection services)
	{
		var serviceProvider = services.BuildServiceProvider();
		var userManager = serviceProvider.GetService<UserManager<User>>() ?? throw new Exception("UserManager not found in ServiceCollection");
		var rootUser = userManager?.Users.FirstOrDefault(u => u.NormalizedEmail == ROOT_ADMIN_EMAIL);
		if (rootUser == null)
		{
			rootUser = new User(ROOT_ADMIN)
			{
				NormalizedUserName = ROOT_ADMIN_EMAIL,
				Email = ROOT_ADMIN_EMAIL,
				NormalizedEmail = ROOT_ADMIN_EMAIL
			};
			var rootUserCreateResult = userManager!.CreateAsync(rootUser, "s0me_Super_secert_p@55w0rd").Result;
			if (!rootUserCreateResult.Succeeded) throw new Exception(string.Join(';', rootUserCreateResult.Errors));
			var addRoleResult = userManager.AddToRoleAsync(rootUser, ConstRoles.Admin).Result;
			if (!addRoleResult.Succeeded) throw new Exception(string.Join(';', addRoleResult.Errors));
		}
		return services;
	}
}
