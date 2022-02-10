using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.Domain.Interfaces
{
    public interface ITodoItemsRepository
    {
        public Task<List<TodoItem>> GetAsync();

        public Task<TodoItem> GetAsync(string id);

        public Task<TodoItem> CreateAsync(TodoItem item);

        public Task UpdateAsync(string id, TodoItem item);

        public Task DeleteAsync(string id);
    }
}
