﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.UsersIdentity.Context;
using ShiftPlan.UsersIdentity.Models;
using System.Security.Claims;

namespace ShiftPlan.UsersIdentity.Controllers;

[ApiController]
[Route("api/identity/roles")]
[Authorize]
public class UserRolesController(
	RoleManager<IdentityRole> roleManager,
	IdentityUserContext context) : ControllerBase
{
	[HttpGet]
	public IActionResult GetRoles()
	{
		var user = this.HttpContext.User;
		if (user.Identity is not null && user.Identity.IsAuthenticated)
		{
			var identity = (ClaimsIdentity)user.Identity;
			var roles = identity.FindAll(identity.RoleClaimType)
				.Select(c =>
					new
					{
						c.Issuer,
						c.OriginalIssuer,
						c.Type,
						c.Value,
						c.ValueType
					});

			return Ok(roles);
		}

		return Unauthorized();
	}

	[HttpGet("all")]
	public ActionResult<List<IdentityRole>> GetAllRoles() => Ok(context.Roles.ToList());

	[HttpPost("add")]
	[Authorize(Roles = RolesNames.Admin)]
	public async Task<IActionResult> AddRole([FromBody] string roleName)
	{
		var role = new IdentityRole(roleName)
		{
			NormalizedName = roleName.ToUpper()
		};

		await roleManager.CreateAsync(role);
		return Ok(role);
	}

	[HttpDelete("delete/{roleName}")]
	[Authorize(Roles = RolesNames.Admin)]
	public async Task<IActionResult> DeleteRole(string roleName)
	{
		var role = await roleManager.FindByNameAsync(roleName);
		if (role is null) return NotFound($"Role {roleName} not found");

		var result = await roleManager.DeleteAsync(role);
		if (result.Succeeded) return Ok(result);
		return BadRequest(result);
	}
}
