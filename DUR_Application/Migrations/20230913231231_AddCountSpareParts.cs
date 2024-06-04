using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUR_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddCountSpareParts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "SpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "SpareParts");
        }
    }
}
