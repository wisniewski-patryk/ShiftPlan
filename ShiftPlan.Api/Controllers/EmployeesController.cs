using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.Api.Models;
using ShiftPlan.Api.Repository;

namespace ShiftPlan.Api.Controllers;

[ApiController]
[Route("api/employees")]
[Authorize]
public class EmployeesController(IRepository<Employee> employeesRepository) : ControllerBase
{
	[HttpGet, AllowAnonymous]
	public ActionResult<IAsyncEnumerable<Employee>> GetAll()
	{
		var employees = employeesRepository.GetAll();
		return Ok(employees);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Employee>> Get(int id) => Ok(await employeesRepository.Get(id));

	[HttpPost("insertOrUpdate")]
	public async Task<ActionResult<Employee>> InsertOrUpdate([FromBody] Employee employee) => Ok(await employeesRepository.InsertOrUpdate(employee));

	[HttpDelete("delete")]
	public async Task<IActionResult> Delete([FromBody] Employee employee)
	{
		await employeesRepository.Delete(employee);
		return Ok();
	}
}
