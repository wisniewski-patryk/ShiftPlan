﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.Api.Models;
using ShiftPlan.Api.Repository;

namespace ShiftPlan.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController(IRepository<Employee> employeesRepository) : ControllerBase
{
	[HttpGet]
	public ActionResult<IAsyncEnumerable<Employee>> GetAll()
	{
		var employees = employeesRepository.GetAll();
		return Ok(employees);
	}

	[HttpGet("{id}"), Authorize]
	public async Task<ActionResult<Employee>> Get(int id) => Ok(await employeesRepository.Get(id));

	[HttpPost("insertOrUpdate"), Authorize]
	public async Task<ActionResult<Employee>> InsertOrUpdate([FromBody] Employee employee) => Ok(await employeesRepository.InsertOrUpdate(employee));

	[HttpDelete("delete"), Authorize]
	public async Task<IActionResult> Delete([FromBody] Employee employee)
	{
		await employeesRepository.Delete(employee);
		return Ok();
	}
}
