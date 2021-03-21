using Microsoft.EntityFrameworkCore.Migrations;

namespace Gallery.Data.Migrations
{
    public partial class AddCategoryToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Images",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Caption",
                table: "Images",
                newName: "Format");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CategoryImage",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "int", nullable: false),
                    ImagesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryImage", x => new { x.CategoriesID, x.ImagesID });
                    table.ForeignKey(
                        name: "FK_CategoryImage_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryImage_Images_ImagesID",
                        column: x => x.ImagesID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImage_ImagesID",
                table: "CategoryImage",
                column: "ImagesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryImage");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Images",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Images",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "Format",
                table: "Images",
                newName: "Caption");
        }
    }
}
