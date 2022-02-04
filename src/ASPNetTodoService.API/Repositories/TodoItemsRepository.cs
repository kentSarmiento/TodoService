using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetTodoService.API.Entities;
using ASPNetTodoService.API.Datastores;

namespace ASPNetTodoService.API.Repositories
{
    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly ITodoItemsDatastore _todoItemsDatastore;

        public TodoItemsRepository(ITodoItemsDatastore todoItemsDatastore)
        {
            _todoItemsDatastore = todoItemsDatastore;
        }

        public List<TodoItem> Get() =>
            _todoItemsDatastore.Get();

        public TodoItem Get(string id) =>
            _todoItemsDatastore.Get(id);

        public TodoItem Create(TodoItem item) =>
            _todoItemsDatastore.Create(item);

        public void Update(string id, TodoItem item) =>
            _todoItemsDatastore.Update(id, item);

        public void Delete(string id) =>
            _todoItemsDatastore.Delete(id);
    }
}
