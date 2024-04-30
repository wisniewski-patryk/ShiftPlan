using Microsoft.EntityFrameworkCore;
using ShiftPlan.Api.Models;

namespace ShiftPlan.Api.Context;

public class ShiftPlanContext(DbContextOptions<ShiftPlanContext> options)
		: DbContext(options)
{
	public DbSet<Shift> Shifts { get; set; }
	public DbSet<Employee> Employs { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Shift>().HasKey(s => s.Id);
		modelBuilder.Entity<Employee>().HasKey(e => e.Id);
	}
}
