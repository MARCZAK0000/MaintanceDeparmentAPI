using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUR_Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMalfunctionRequestV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests");

            migrationBuilder.AlterColumn<int>(
                name: "RequestStatusId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "RequestStatusId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatus",
                principalColumn: "ID");
        }
    }
}
