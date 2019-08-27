using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApplication.Data.Migrations
{
    public partial class CrateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CatID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    AssignmentDate = table.Column<long>(nullable: false),
                    CreateDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Work" });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Home" });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Personal" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoCategory");

            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
