using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeTracker.Data.Migrations
{
    public partial class AddJobToEntriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "Entries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_JobId",
                table: "Entries",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Jobs_JobId",
                table: "Entries",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Jobs_JobId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_JobId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Entries");
        }
    }
}
