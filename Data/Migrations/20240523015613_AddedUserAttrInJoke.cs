using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserAttrInJoke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Joke",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Joke_UserId",
                table: "Joke",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joke_AspNetUsers_UserId",
                table: "Joke",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joke_AspNetUsers_UserId",
                table: "Joke");

            migrationBuilder.DropIndex(
                name: "IX_Joke_UserId",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Joke");
        }
    }
}
