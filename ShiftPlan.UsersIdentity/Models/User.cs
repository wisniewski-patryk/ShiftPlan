using Microsoft.AspNetCore.Identity;

namespace ShiftPlan.UsersIdentity.Models;

public class User : IdentityUser
{
	public User() : base()
	{
	}

	public User(string userName) : base(userName)
	{
	}
}
