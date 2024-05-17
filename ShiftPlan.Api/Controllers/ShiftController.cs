using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Models;
using ShiftPlan.Api.Repository;

namespace ShiftPlan.Api.Controllers;

[ApiController]
[Route("api/shifts")]
public class ShiftController(IRepository<Shift> shiftsRepository) : ControllerBase
{
	[HttpGet]
	public ActionResult<IAsyncEnumerable<Shift>> GetAll()
	{
		var shifts = shiftsRepository.GetAll().Include(s => s.Employee);
		return Ok(shifts);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Shift>> Get(int id) => Ok(await shiftsRepository.Get(id));

	[HttpPost("insertOrUpdate")]
	public async Task<ActionResult<Shift>> InsertOrUpdate([FromBody] Shift shift) =>
		Ok(await shiftsRepository.InsertOrUpdate(shift));

	[HttpDelete("delete")]
	public async Task<IActionResult> Delete([FromBody] Shift shift) 
	{
		await shiftsRepository.Delete(shift);
		return Ok();
	}
}
