using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ShiftPlan.UsersIdentity.Users;

public interface IUserContext
{
	CurrentUser GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
	public CurrentUser GetCurrentUser()
	{
		var user = (httpContextAccessor.HttpContext?.User) ?? throw new InvalidOperationException("User context is not present");

		if (user.Identity is null || !user.Identity.IsAuthenticated)
			throw new InvalidOperationException("User is not authenticated");

		var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("User id not exists");
		var email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value ?? throw new Exception("User email not exists");
		var roles = user.FindAll(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

		return new CurrentUser(userId, email, roles);
	}
}
