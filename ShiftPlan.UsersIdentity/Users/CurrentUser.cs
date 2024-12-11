namespace ShiftPlan.UsersIdentity.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
{
	public bool IsInRole(string role) => this.Roles.Contains(role);
}
