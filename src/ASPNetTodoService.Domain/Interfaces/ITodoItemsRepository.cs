﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.Domain.Interfaces
{
    public enum RepositoryType
    {
        MongoDb,
        SqlInMemory,
        Sqlite,
    }

    public interface ITodoItemsRepository
    {
        public Task<List<TodoItem>> GetAsync();

        public Task<TodoItem> GetAsync(string id);

        public Task<TodoItem> CreateAsync(TodoItem item);

        public Task UpdateAsync(string id, TodoItem item);

        public Task DeleteAsync(string id);
    }
}
