using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ASPNetTodoService.Domain.Entities;
using ASPNetTodoService.Domain.Interfaces;
using System.Threading.Tasks;

namespace ASPNetTodoService.Infrastructure.Repositories
{
    public class TodoItemsMongoRepository : ITodoItemsRepository
    {
        private readonly IMongoCollection<DatabaseItem> _todoItems;

        public TodoItemsMongoRepository(ITodoItemsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _todoItems = database.GetCollection<DatabaseItem>(settings.TodoItemsCollectionName);
        }

        public async Task<List<TodoItem>> GetAsync()
        {
            List<DatabaseItem> items = await _todoItems.Find(todoItem => true).ToListAsync();

            List<TodoItem> response = items.Select(item => new TodoItem()
            {
                Id = item.Id,
                TaskName = item.Name,
                Done = item.Done,
            }).ToList();

            return response;
        }

        public async Task<TodoItem> GetAsync(string id)
        {
            DatabaseItem item = await _todoItems.Find<DatabaseItem>(todoItem => todoItem.Id == id).FirstOrDefaultAsync();

            TodoItem response = new TodoItem()
            {
                Id = item.Id,
                TaskName = item.Name,
                Done = item.Done,
            };

            return response;
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            DatabaseItem databaseItem = new DatabaseItem
            {
                Name = item.TaskName,
                Done = item.Done,
            };

            await _todoItems.InsertOneAsync(databaseItem);
            item.Id = databaseItem.Id;

            return item;
        }

        public async Task UpdateAsync(string id, TodoItem item)
        {
            DatabaseItem databaseItem = new DatabaseItem
            {
                Id = item.Id,
                Name = item.TaskName,
                Done = item.Done,
            };

            await _todoItems.ReplaceOneAsync(todoItem => todoItem.Id == id, databaseItem);
        }

        public async Task DeleteAsync(string id) =>
            await _todoItems.DeleteOneAsync(todoItem => todoItem.Id == id);
    }

    public class DatabaseItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
    }

}

