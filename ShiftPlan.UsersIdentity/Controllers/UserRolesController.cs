using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.UsersIdentity.Context;
using ShiftPlan.UsersIdentity.Models;

namespace ShiftPlan.UsersIdentity.Controllers;

[ApiController]
[Route("api/identity/roles")]
[Authorize(Roles = ConstRoles.Admin)]
internal class UserRolesController(
	RoleManager<IdentityRole> roleManager,
	IdentityUserContext context) : ControllerBase
{
	[HttpGet]
	public ActionResult<List<IdentityRole>> GetAllRoles() => Ok(context.Roles.ToList());

	[HttpPost("add")]
	public async Task<IActionResult> AddRole([FromBody] string roleName)
	{
		var role = new IdentityRole(roleName)
		{
			NormalizedName = roleName.ToUpper()
		};

		await roleManager.CreateAsync(role);
		return Ok(role);
	}

	[HttpDelete("delete")]
	public async Task<IActionResult> DeleteRole([FromBody] string roleName)
	{
		var role = await roleManager.FindByNameAsync(roleName);
		if (role is null) return NotFound($"Role {roleName} not found");

		var result = await roleManager.DeleteAsync(role);
		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}
}
