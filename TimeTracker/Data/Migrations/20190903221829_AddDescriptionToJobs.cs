using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class AddDescriptionToJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Jobs");
        }
    }
}
