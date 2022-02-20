using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetTodoService.Infrastructure.Migrations
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
