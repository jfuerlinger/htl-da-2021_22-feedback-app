using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackApp.Persistence.Migrations
{
    public partial class UpdateStatisticModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCount",
                table: "GlobalHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCount",
                table: "GlobalHistories");
        }
    }
}
