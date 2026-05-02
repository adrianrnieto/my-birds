using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBirds.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_Name",
                table: "Orders",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Genus_Name",
                table: "Genera",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Family_Name",
                table: "Families",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_Name",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Genus_Name",
                table: "Genera");

            migrationBuilder.DropIndex(
                name: "IX_Family_Name",
                table: "Families");
        }
    }
}
