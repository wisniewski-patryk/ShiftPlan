using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftPlan.UsersIdentity.Migrations
{
	/// <inheritdoc />
	public partial class UserAdditionalProperties : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateOnly>(
				name: "DatOfBirth",
				table: "AspNetUsers",
				type: "date",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Nationality",
				table: "AspNetUsers",
				type: "text",
				nullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "DatOfBirth",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "Nationality",
				table: "AspNetUsers");
		}
	}
}
