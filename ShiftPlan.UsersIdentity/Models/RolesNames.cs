namespace ShiftPlan.UsersIdentity.Models;

public static class RolesNames // TODO: rewrite to enum (with ToString serialization - RolesNames.Admin in json should be "Admin")
{
	public const string Admin = "Admin";
	public const string Editor = "Editor";
	public const string Viewer = "Viewer";
}
