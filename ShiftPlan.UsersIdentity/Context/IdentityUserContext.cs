using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShiftPlan.UsersIdentity.Models;

namespace ShiftPlan.UsersIdentity.Context;

public class IdentityUserContext(DbContextOptions<IdentityUserContext> options) : IdentityDbContext<Models.User>(options)
{
	override protected void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		var adminUserGuid = Guid.NewGuid().ToString();
		var adminRoleGuid = Guid.NewGuid().ToString();
		modelBuilder.Entity<IdentityRole>()
			.HasData(new IdentityRole
			{
				Id = adminRoleGuid,
				Name = "Admin",
				NormalizedName = "ADMIN"
			});

		modelBuilder.Entity<User>()
			.HasData(new User
			{
				Id = adminUserGuid,
				UserName = "ROOT_ADMIN@root.local",
				NormalizedUserName = "ROOT_ADMIN@ROOT.LOCAL",
				Email = "ROOT_ADMIN@root.local",
				NormalizedEmail = "ROOT_ADMIN@ROOT.LOCAL",
				PasswordHash = new PasswordHasher<User>().HashPassword(default!, "ROOT_ADMIN_PASSWORD"),
				SecurityStamp = Guid.NewGuid().ToString()
			});

		modelBuilder.Entity<IdentityUserRole<string>>()
			.HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleGuid,
				UserId = adminUserGuid
			});
	}
}
