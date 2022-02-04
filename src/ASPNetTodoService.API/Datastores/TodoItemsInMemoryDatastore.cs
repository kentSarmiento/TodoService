using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ASPNetTodoService.API.Entities;

namespace ASPNetTodoService.API.Datastores
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
            item.Id = Guid.NewGuid().ToString();

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return item;
        }

        public void Update(string id, TodoItem item)
        {

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            var todoItem = _context.TodoItems.Find(id);
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
        }
    }
}
