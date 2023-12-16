using Microsoft.EntityFrameworkCore;
using TodoService.Domain.Entities;

namespace TodoService.Infrastructure.Repositories
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