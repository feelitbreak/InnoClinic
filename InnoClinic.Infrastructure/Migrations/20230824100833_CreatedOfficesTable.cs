using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedOfficesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Housenumber = table.Column<string>(name: "House number", type: "nvarchar(max)", nullable: false),
                    Officenumber = table.Column<string>(name: "Office number", type: "nvarchar(max)", nullable: false),
                    Registryphonenumber = table.Column<int>(name: "Registry phone number", type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
