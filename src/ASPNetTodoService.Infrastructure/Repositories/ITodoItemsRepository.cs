using System.Collections.Generic;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.Infrastructure.Repositories
{
    public interface ITodoItemsRepository
    {
        public List<TodoItem> Get();

        public TodoItem Get(string id);

        public TodoItem Create(TodoItem item);

        public void Update(string id, TodoItem item);

        public void Delete(string id);
    }
}
