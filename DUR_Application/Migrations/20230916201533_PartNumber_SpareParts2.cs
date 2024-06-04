using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUR_Application.Migrations
{
    /// <inheritdoc />
    public partial class PartNumber_SpareParts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "SpareParts");
        }
    }
}
