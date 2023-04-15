using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizItemId1",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuizItemId1",
                table: "UserAnswers",
                column: "QuizItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizItems_QuizItemId1",
                table: "UserAnswers",
                column: "QuizItemId1",
                principalTable: "QuizItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizItems_QuizItemId1",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuizItemId1",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "QuizItemId1",
                table: "UserAnswers");
        }
    }
}
