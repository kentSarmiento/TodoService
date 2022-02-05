using Microsoft.EntityFrameworkCore;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.Infrastructure.Datastores
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}