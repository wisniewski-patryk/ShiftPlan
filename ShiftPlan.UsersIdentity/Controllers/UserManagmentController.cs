using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.UsersIdentity.Context;
using ShiftPlan.UsersIdentity.Models;

namespace ShiftPlan.UsersIdentity.Controllers;

[ApiController]
[Route("api/identity/users")]
[Authorize(Roles = RolesNames.Admin)]
public class UserManagmentController(
	UserManager<User> userManager,
	IdentityUserContext context) : ControllerBase
{
	[HttpGet]
	public IActionResult GetAllUsers()
	{
		return Ok(context.Users.ToList());
	}

	[HttpGet("withroles")]
	public async Task<IActionResult> GetAllUsersWithRoles()
	{
		var users = context.Users.ToList();
		var usersWithRoles = new List<UserWithRoles>();
		foreach (var user in users)
		{
			var userRoles = await userManager.GetRolesAsync(user);
			usersWithRoles.Add(new UserWithRoles(user, userRoles));
		}
		return Ok(usersWithRoles);
	}

	[HttpDelete("delete")]
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

	[HttpPatch("edit")]
	public async Task<IActionResult> EditUser([FromBody] EditUserRequest req)
	{
		var userToEdit = await userManager.FindByEmailAsync(req.UserEmail);
		if (userToEdit is null)
			return NotFound("User to edit hasn't been found.");

		userToEdit.Email = req.NewEmailAddress;
		if (req.NewPhoneNumber is not null)
			userToEdit.PhoneNumber = req.NewPhoneNumber;

		var result = await userManager.UpdateAsync(userToEdit);

		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}

	[HttpGet("assigment")]
	public async Task<IActionResult> GetUserAssigmentRoles([FromQuery] GetUserRolesRequest user)
	{
		var userData = await userManager.FindByEmailAsync(user.UserEmail);
		if (userData is null) return NotFound("User not found");

		var result = await userManager.GetRolesAsync((User)userData);
		return Ok(result);
	}

	[HttpPost("assigment/add")]
	public async Task<IActionResult> AssignRoleToUser([FromBody] RoleAssignmentRequest roleAssigmentRequest)
	{
		var user = await userManager.FindByEmailAsync(roleAssigmentRequest.UserEmail);
		if (user is null)
			return NotFound("User not found");

		var userRoles = await userManager.GetRolesAsync(user);
		if (userRoles.Contains(roleAssigmentRequest.RoleName))
			return BadRequest("User already has this role");

		var result = await userManager.AddToRoleAsync(user, roleAssigmentRequest.RoleName);
		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}

	[HttpPost("assigment/remove")]
	public async Task<IActionResult> RemoveAssignRoleToUser([FromBody] RemoveAssigmentRequest removeAssigmentRequest)
	{
		var user = await userManager.FindByEmailAsync(removeAssigmentRequest.UserEmail);
		if (user is null) return NotFound("User not found");

		var result = await userManager.RemoveFromRoleAsync(user, removeAssigmentRequest.RoleName);
		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}
}

public record UserWithRoles(User User, IEnumerable<string> Roles);
public record DeleteUserRequest(string UserEmail);
public record EditUserRequest(string UserEmail, string NewEmailAddress, string? NewPhoneNumber);
public record GetUserRolesRequest(string UserEmail);
public record RoleAssignmentRequest(string UserEmail, string RoleName);
public record RemoveAssigmentRequest(string UserEmail, string RoleName);
