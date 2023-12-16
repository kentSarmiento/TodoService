using TodoService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class TodoContextFactory : IDesignTimeDbContextFactory<TodoContext>
{
    public TodoContext CreateDbContext(string[] args)
    {
        //var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
        //var connectionString = "Data Source=Todos.db";
        //optionsBuilder.UseSqlite(connectionString);

        //return new TodoContext(optionsBuilder.Options);
        return CreateDbContext();
    }

    public static TodoContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
        var connectionString = "Data Source=..\\Todos.db";
        optionsBuilder.UseSqlite(connectionString);

        return new TodoContext(optionsBuilder.Options);
    }
}