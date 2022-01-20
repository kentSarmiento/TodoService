using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ASPNetCore5TodoAPI.Entities;

namespace ASPNetCore5TodoAPI.Datastores
{
    public class TodoItemsInMemoryDatastore : ITodoItemsDatastore
    {
        private readonly TodoContext _context;
        public TodoItemsInMemoryDatastore(TodoContext context)
        {
            _context = context;
        }

        public List<TodoItem> Get()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem Get(string id)
        {
            return _context.TodoItems.Find(id);
        }

        public TodoItem Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Update(string id, TodoItem item)
        {
        }

        public void Delete(string id)
        {
        }
    }
}
