using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftPlan.UsersIdentity.Migrations
{
	/// <inheritdoc />
	public partial class UserAdditionalPropertiesFixTypo : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "DatOfBirth",
				table: "AspNetUsers",
				newName: "DateOfBirth");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "DateOfBirth",
				table: "AspNetUsers",
				newName: "DatOfBirth");
		}
	}
}
