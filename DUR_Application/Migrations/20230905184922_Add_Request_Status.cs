using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUR_Application.Migrations
{
    /// <inheritdoc />
    public partial class Add_Request_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestStatusId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MalfunctionRequests_RequestStatusId",
                table: "MalfunctionRequests",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropIndex(
                name: "IX_MalfunctionRequests_RequestStatusId",
                table: "MalfunctionRequests");

            migrationBuilder.DropColumn(
                name: "RequestStatusId",
                table: "MalfunctionRequests");
        }
    }
}
