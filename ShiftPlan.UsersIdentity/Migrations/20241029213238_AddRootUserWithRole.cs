using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftPlan.UsersIdentity.Migrations
{
    /// <inheritdoc />
    public partial class AddRootUserWithRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ba812a7-68af-4e8a-be59-c76fa61a7105", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "97f21273-8281-4109-ae1d-1e528ae47b8f", 0, "8896e314-300b-4c43-b8d6-d64325deeb64", "ROOT_ADMIN@root.local", false, false, null, "ROOT_ADMIN@ROOT.LOCAL", "ROOT_ADMIN@ROOT.LOCAL", "AQAAAAIAAYagAAAAEFhUKiE/gqZ1nkUcrG7Y6bPn5iwDd23yJsh1Lwt4jxya6uTXoDfc6ni7etLABHCeww==", null, false, "259962aa-da73-40b8-b75b-59fc3dc1c4c2", false, "ROOT_ADMIN@root.local" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7ba812a7-68af-4e8a-be59-c76fa61a7105", "97f21273-8281-4109-ae1d-1e528ae47b8f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7ba812a7-68af-4e8a-be59-c76fa61a7105", "97f21273-8281-4109-ae1d-1e528ae47b8f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ba812a7-68af-4e8a-be59-c76fa61a7105");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97f21273-8281-4109-ae1d-1e528ae47b8f");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
