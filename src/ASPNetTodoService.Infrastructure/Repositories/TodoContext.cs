using Microsoft.EntityFrameworkCore;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.Infrastructure.Repositories
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}