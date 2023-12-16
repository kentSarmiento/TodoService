using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoService.Infrastructure.Migrations
{
    public partial class RenameIsCompleteToDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "TodoItems",
                newName: "Done");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Done",
                table: "TodoItems",
                newName: "IsComplete");
        }
    }
}
