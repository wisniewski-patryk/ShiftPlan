using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftPlan.Api.Models;

public class BaseEntity
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
}
