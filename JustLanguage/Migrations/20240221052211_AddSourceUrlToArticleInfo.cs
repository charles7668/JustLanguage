using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustLanguage.Migrations
{
    /// <inheritdoc />
    public partial class AddSourceUrlToArticleInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SrcUrl",
                table: "ArticleInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SrcUrl",
                table: "ArticleInfo");
        }
    }
}
