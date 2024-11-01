using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.UsersIdentity.Context;
using ShiftPlan.UsersIdentity.Models;

namespace ShiftPlan.UsersIdentity.Controllers;

[ApiController]
[Route("api/identity/users")]
[Authorize(Roles = ConstRoles.Admin)]
public class UserManagmentController(
	UserManager<User> userManager,
	IdentityUserContext context) : ControllerBase
{
	[HttpGet]
	public IActionResult GetAllUsers()
	{
		return Ok(context.Users.ToList());
	}

	[HttpDelete]
	[Route("delete")]
	public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest req)
	{
		var userToDelete = await userManager.FindByEmailAsync(req.UserEmail);
		if (userToDelete is null)
			return NotFound("User to delete hasn't been found.");

		var userRoles = await userManager.GetRolesAsync(userToDelete);
		var rolesRemoveResult = await userManager.RemoveFromRolesAsync(userToDelete, userRoles);
		if (!rolesRemoveResult.Succeeded)
			return BadRequest("Can't remove roles from user");

		var result = await userManager.DeleteAsync(userToDelete);

		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}

	[HttpPatch("assigment/add")]
	public async Task<IActionResult> AssignRoleToUser([FromBody] RoleAssignmentRequest assignmentObject)
	{
		var user = await userManager.FindByEmailAsync(assignmentObject.UserEmail);
		if (user is null)
			return NotFound("User not found");

		var userRoles = await userManager.GetRolesAsync(user);
		if (userRoles.Contains(assignmentObject.RoleName))
			return BadRequest("User already has this role");

		var result = await userManager.AddToRoleAsync(user, assignmentObject.RoleName);
		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}

	[HttpPatch("assigment/remove")]
	public async Task<IActionResult> RemoveAssignRoleToUser([FromBody] RemoveAssigmentRequest removeAssigmentRequest)
	{
		var user = await userManager.FindByEmailAsync(removeAssigmentRequest.UserEmail);
		if (user is null) return NotFound("User not found");

		var result = await userManager.RemoveFromRoleAsync(user, removeAssigmentRequest.RoleName);
		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}
}
public record DeleteUserRequest(string UserEmail);
public record RoleAssignmentRequest(string UserEmail, string RoleName);
public record RemoveAssigmentRequest(string UserEmail, string RoleName);
