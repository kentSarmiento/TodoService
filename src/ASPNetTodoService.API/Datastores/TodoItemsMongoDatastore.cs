using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ASPNetTodoService.API.Entities;

namespace ASPNetTodoService.API.Datastores
{
    public class TodoItemsMongoDatastore : ITodoItemsDatastore
    {
        private readonly IMongoCollection<DatabaseItem> _todoItems;
        private readonly ILogger _logger;

        public TodoItemsMongoDatastore(ITodoItemsDatabaseSettings settings, ILogger<TodoItemsMongoDatastore> logger)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _todoItems = database.GetCollection<DatabaseItem>(settings.TodoItemsCollectionName);
            _logger = logger;
        }

        public List<TodoItem> Get()
        {
            List<DatabaseItem> items = _todoItems.Find(todoItem => true).ToList();

            List<TodoItem> response = items.Select(item => new TodoItem()
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
            }).ToList();

            return response;
        }

        public TodoItem Get(string id)
        {
            DatabaseItem item = _todoItems.Find<DatabaseItem>(todoItem => todoItem.Id == id).FirstOrDefault();

            TodoItem response = new TodoItem()
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
            };

            return response;
        }

        public TodoItem Create(TodoItem item)
        {
            DatabaseItem databaseItem = new DatabaseItem
            {
                Name = item.Name,
                IsComplete = item.IsComplete,
            };

            _todoItems.InsertOne(databaseItem);
            item.Id = databaseItem.Id;

            return item;
        }

        public void Update(string id, TodoItem item)
        {
            DatabaseItem databaseItem = new DatabaseItem
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete,
            };

            _todoItems.ReplaceOne(todoItem => todoItem.Id == id, databaseItem);
        }

        public void Delete(string id) =>
            _todoItems.DeleteOne(todoItem => todoItem.Id == id);
    }

    public class DatabaseItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

}

