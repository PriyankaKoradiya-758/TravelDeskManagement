using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDeskManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelBookings_TravelRequests_TravelRequestRequestId",
                table: "TravelBookings");

            migrationBuilder.DropIndex(
                name: "IX_TravelBookings_TravelRequestRequestId",
                table: "TravelBookings");

            migrationBuilder.DropColumn(
                name: "TravelRequestRequestId",
                table: "TravelBookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelRequestRequestId",
                table: "TravelBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TravelBookings_TravelRequestRequestId",
                table: "TravelBookings",
                column: "TravelRequestRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelBookings_TravelRequests_TravelRequestRequestId",
                table: "TravelBookings",
                column: "TravelRequestRequestId",
                principalTable: "TravelRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
