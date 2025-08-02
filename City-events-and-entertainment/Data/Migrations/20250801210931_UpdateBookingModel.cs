using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace City_events_and_entertainment.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Museums_Teams_TeamId",
                table: "Museums");

            migrationBuilder.AddForeignKey(
                name: "FK_Museums_Teams_TeamId",
                table: "Museums",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Museums_Teams_TeamId",
                table: "Museums");

            migrationBuilder.AddForeignKey(
                name: "FK_Museums_Teams_TeamId",
                table: "Museums",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
