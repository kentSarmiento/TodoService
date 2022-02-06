using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using ASPNetTodoService.API.DTOs;

namespace ASPNetTodoService.Specs.Drivers
{
    public class TodoServiceRequestDriver
    {
        private readonly string BASE_URL = "https://localhost:5001";
        private readonly string ROUTE = "/api/todoitems";

        protected readonly string TODO_ITEM_NAME = "Specflow Testing";

        private readonly RestClient _client;

        public TodoServiceRequestDriver()
        {
            _client = new RestClient(BASE_URL);
        }

        public async Task<List<TodoItemDTO>?> RetrieveTodoItems()
        {
            var request = new RestRequest(ROUTE);
            var response = await _client.GetAsync<TodoItemDTO[]>(request);

            return response?.ToList();
        }

        public async Task<TodoItemDTO?> RegisterTodoItem()
        {
            CreateTodoItemDTO newTodo = new CreateTodoItemDTO() { Name = TODO_ITEM_NAME, IsComplete = true };

            var request = new RestRequest(ROUTE).AddJsonBody(newTodo);
            var response = await _client.PostAsync<TodoItemDTO>(request);

            return response;
        }

        public async Task ClearTodoItems()
        {
            var response = await RetrieveTodoItems();
            if (response != null)
            {
                foreach (var todoItem in response)
                {
                    var request = new RestRequest($"{ROUTE}/{todoItem.Id}");
                    await _client.DeleteAsync(request);
                }
            }
        }
    }
}
