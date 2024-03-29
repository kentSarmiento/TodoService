﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoService.Domain.Entities;
using TodoService.Domain.Interfaces;
using System.Threading.Tasks;

namespace TodoService.Infrastructure.Repositories
{
    public class TodoItemsEFRepository : ITodoItemsRepository
    {
        private readonly TodoContext _context;
        public TodoItemsEFRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetAsync(string id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            item.Id = Guid.NewGuid().ToString();

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task UpdateAsync(string id, TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            var todoItem = _context.TodoItems.Find(id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}
