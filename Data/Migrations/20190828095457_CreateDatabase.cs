using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApplication.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    CreatedDay = table.Column<long>(nullable: false)
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
                    Name = table.Column<string>(nullable: true),
                    Complete = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<long>(nullable: false),
                    CreateDate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "ID", "CreatedDay", "Name" },
                values: new object[] { 1, 0L, "Work" });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "ID", "CreatedDay", "Name" },
                values: new object[] { 2, 0L, "Home" });

            migrationBuilder.InsertData(
                table: "TodoCategory",
                columns: new[] { "ID", "CreatedDay", "Name" },
                values: new object[] { 3, 0L, "Personal" });
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
