using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishLearning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizAssignmentsHistoryLeaderboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leaderboards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    QuizzesCompleted = table.Column<int>(type: "int", nullable: false),
                    AverageScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Streak = table.Column<int>(type: "int", nullable: false),
                    LastActiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetRole = table.Column<int>(type: "int", nullable: true),
                    TargetUserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAssignments_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 7, 6, 34, 16, 644, DateTimeKind.Utc).AddTicks(3022), "$2a$11$9iuEFUXFa87PO6wabg9e6uLCe973ldUe1729uRkM5XUWB81BGogd.", new DateTime(2026, 7, 7, 6, 34, 16, 644, DateTimeKind.Utc).AddTicks(3023) });

            migrationBuilder.CreateIndex(
                name: "IX_Leaderboards_UserId",
                table: "Leaderboards",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearningHistories_CreatedAt",
                table: "LearningHistories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_LearningHistories_UserId",
                table: "LearningHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAssignments_EndTime",
                table: "QuizAssignments",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAssignments_QuizId",
                table: "QuizAssignments",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAssignments_StartTime",
                table: "QuizAssignments",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAssignments_TargetRole",
                table: "QuizAssignments",
                column: "TargetRole");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAssignments_TargetUserId",
                table: "QuizAssignments",
                column: "TargetUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaderboards");

            migrationBuilder.DropTable(
                name: "LearningHistories");

            migrationBuilder.DropTable(
                name: "QuizAssignments");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Quizzes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 7, 7, 5, 50, 25, 233, DateTimeKind.Utc).AddTicks(5986), "$2a$11$sMZhcxblhWg9BuQj8jM6Zu7ZR3QCsJ2Ba4cf9dAz5UheQ3BEFYBYq", new DateTime(2026, 7, 7, 5, 50, 25, 233, DateTimeKind.Utc).AddTicks(5986) });
        }
    }
}
