using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBirds.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class v6_adding_favourites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Photos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarred",
                table: "Photos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "IsStarred",
                table: "Photos");
        }
    }
}
