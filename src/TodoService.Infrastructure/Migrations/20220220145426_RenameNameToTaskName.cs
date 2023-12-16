using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoService.Infrastructure.Migrations
{
    public partial class RenameNameToTaskName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TodoItems",
                newName: "TaskName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "TodoItems",
                newName: "Name");
        }
    }
}
