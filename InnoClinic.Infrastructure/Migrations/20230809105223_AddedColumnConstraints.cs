using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReenteredPassword",
                table: "User",
                newName: "Re-entered Password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "E-mail");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Re-entered Password",
                table: "User",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "E-mail",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_User_E-mail",
                table: "User",
                column: "E-mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_E-mail",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Re-entered Password",
                table: "User",
                newName: "ReenteredPassword");

            migrationBuilder.RenameColumn(
                name: "E-mail",
                table: "User",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReenteredPassword",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "");
        }
    }
}
