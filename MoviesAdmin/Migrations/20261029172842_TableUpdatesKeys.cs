using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class TableUpdatesKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_CriticReview_CriticReviewID",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_CriticReviewID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "CriticReviewID",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "CriticReview",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CriticReview_MovieID",
                table: "CriticReview",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_CriticReview_Movie_MovieID",
                table: "CriticReview",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CriticReview_Movie_MovieID",
                table: "CriticReview");

            migrationBuilder.DropIndex(
                name: "IX_CriticReview_MovieID",
                table: "CriticReview");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "CriticReview");

            migrationBuilder.AddColumn<int>(
                name: "CriticReviewID",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_CriticReviewID",
                table: "Movie",
                column: "CriticReviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_CriticReview_CriticReviewID",
                table: "Movie",
                column: "CriticReviewID",
                principalTable: "CriticReview",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
