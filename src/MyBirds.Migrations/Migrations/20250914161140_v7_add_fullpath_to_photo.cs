using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBirds.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class v7_add_fullpath_to_photo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "Photos");
        }
    }
}
