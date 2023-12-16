using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace TodoService.Specs.Drivers
{
    public class TodoServiceRequestDriver
    {
        private readonly string BASE_URL = "http://localhost:5000/";
        private readonly string ROUTE = "api/todoitems";

        private readonly RestClient _client;

        public TodoServiceRequestDriver()
        {
            _client = new RestClient(BASE_URL);
        }

        public async Task<List<TodoItem>?> RetrieveTodoItems()
        {
            var request = new RestRequest(ROUTE);
            var todoList = await _client.GetAsync<TodoItem[]>(request);

            return todoList?.ToList();
        }

        public async Task<TodoItem?> RegisterTodoItem(TodoItem todoItem)
        {
            var request = new RestRequest(ROUTE).AddJsonBody(todoItem);
            return await _client.PostAsync<TodoItem>(request);
        }

        public async Task ClearTodoItems()
        {
            var todoList = await RetrieveTodoItems();
            if (todoList != null)
            {
                foreach (var todoItem in todoList)
                {
                    var request = new RestRequest($"{ROUTE}/{todoItem.Id}");
                    await _client.DeleteAsync(request);
                }
            }
        }
    }
}
