using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackApp.Persistence.Migrations
{
    public partial class AddStatisticModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTeachingUnitsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedFeedbacksCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachingUnitStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackCount = table.Column<int>(type: "int", nullable: false),
                    AvgStars = table.Column<double>(type: "float", nullable: false),
                    TeachingUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingUnitStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingUnitStatistics_TeachingUnits_TeachingUnitId",
                        column: x => x.TeachingUnitId,
                        principalTable: "TeachingUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTeachingUnitsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedFeedbacksCount = table.Column<int>(type: "int", nullable: false),
                    AvgStars = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachingUnitStatistics_TeachingUnitId",
                table: "TeachingUnitStatistics",
                column: "TeachingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalHistories");

            migrationBuilder.DropTable(
                name: "TeachingUnitStatistics");

            migrationBuilder.DropTable(
                name: "UserStatistics");
        }
    }
}
