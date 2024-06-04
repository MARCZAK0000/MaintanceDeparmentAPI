using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DUR_Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMalfunctionsRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MalfunctionRequests_Users_UserId",
                table: "MalfunctionRequests");

            migrationBuilder.AlterColumn<decimal>(
                name: "WorkTime",
                table: "MalfunctionRequests",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "UserNumber",
                table: "MalfunctionRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RequestStatusId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "LayoverTime",
                table: "MalfunctionRequests",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MalfunctionRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "LaneNumber",
                table: "MalfunctionRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatus",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MalfunctionRequests_Users_UserId",
                table: "MalfunctionRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MalfunctionRequests_Users_UserId",
                table: "MalfunctionRequests");

            migrationBuilder.DropColumn(
                name: "LaneNumber",
                table: "MalfunctionRequests");

            migrationBuilder.AlterColumn<decimal>(
                name: "WorkTime",
                table: "MalfunctionRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserNumber",
                table: "MalfunctionRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RequestStatusId",
                table: "MalfunctionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LayoverTime",
                table: "MalfunctionRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MalfunctionRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MalfunctionRequests_RequestStatus_RequestStatusId",
                table: "MalfunctionRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalfunctionRequests_Users_UserId",
                table: "MalfunctionRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
