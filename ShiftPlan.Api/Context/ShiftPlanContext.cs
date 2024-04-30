using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShiftPlan.Api.Models;

namespace ShiftPlan.Api.Context;

public class ShiftPlanContext(DbContextOptions<ShiftPlanContext> options)
		: DbContext(options)
{
	public DbSet<Shift> Shifts { get; set; }
	public DbSet<Employee> Employs { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ShiftConfiguration());
		modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
	}
}

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
	public void Configure(EntityTypeBuilder<Shift> builder)
	{
		builder.HasKey(s => s.Id);
		builder.HasOne(s => s.Employee);
	}
}

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.HasKey(e => e.Id);
	}
}

