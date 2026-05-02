using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBirds.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class v5_updating_indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photo_Name",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Order_Name",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Genus_Name",
                table: "Genera");

            migrationBuilder.DropIndex(
                name: "IX_Family_Name",
                table: "Families");

            migrationBuilder.CreateIndex(
                name: "IX_Species_Name",
                table: "Species",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Species_ScientificName",
                table: "Species",
                column: "ScientificName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_Name",
                table: "Photos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Name",
                table: "Orders",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genus_Name",
                table: "Genera",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Family_Name",
                table: "Families",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Species_Name",
                table: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Species_ScientificName",
                table: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Photo_Name",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Order_Name",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Genus_Name",
                table: "Genera");

            migrationBuilder.DropIndex(
                name: "IX_Family_Name",
                table: "Families");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_Name",
                table: "Photos",
                column: "Name");

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
    }
}
