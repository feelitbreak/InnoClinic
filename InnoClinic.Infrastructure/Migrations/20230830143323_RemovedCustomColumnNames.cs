using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCustomColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hashed password",
                table: "Users",
                newName: "HashedPassword");

            migrationBuilder.RenameColumn(
                name: "E-mail",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Registry phone number",
                table: "Offices",
                newName: "RegistryPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Office number",
                table: "Offices",
                newName: "OfficeNumber");

            migrationBuilder.RenameColumn(
                name: "House number",
                table: "Offices",
                newName: "HouseNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Users",
                newName: "Hashed password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "E-mail");

            migrationBuilder.RenameColumn(
                name: "RegistryPhoneNumber",
                table: "Offices",
                newName: "Registry phone number");

            migrationBuilder.RenameColumn(
                name: "OfficeNumber",
                table: "Offices",
                newName: "Office number");

            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "Offices",
                newName: "House number");
        }
    }
}
