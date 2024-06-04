using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUR_Application.Migrations
{
    /// <inheritdoc />
    public partial class updateMachineCs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LaneNumber",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaneNumber",
                table: "Machines");
        }
    }
}
