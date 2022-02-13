using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ASPNetTodoService.API.DTOs;

namespace ASPNetTodoService.IntegrationTest
{
    public class TodoItemsControllerIntegrationTest: ControllerIntegrationTest
    {
        private readonly string ROUTE = "/api/todoitems";

        [SetUp]
        public void Setup() {}

        [Test]
        public async Task GetTodoItems_NoRegisteredTodoItems_ReturnsEmptyListAsync()
        {
            var response = await GenerateServer().CreateClient().GetAsync(ROUTE);

            Assert.IsTrue(response.IsSuccessStatusCode);

            var todoList = JsonConvert.DeserializeObject<GetTodoItemDTO[]>(await response.Content.ReadAsStringAsync());
            Assert.IsEmpty(todoList);
        }

        [Test]
        [Ignore("skip test")]
        public async Task GetTodoItems_WithRegisteredTodoItems_ReturnsNonEmptyList()
        {
            var todoItemDto = new GetTodoItemDTO() { Id = TODO_ITEM_ID, Name = TODO_ITEM_NAME, IsComplete = true };
            var expectedItems = new List<GetTodoItemDTO>() { todoItemDto };

            var response = await GenerateServer().CreateClient().GetAsync(ROUTE);

            Assert.IsTrue(response.IsSuccessStatusCode);

            var todoList = JsonConvert.DeserializeObject<GetTodoItemDTO[]>(await response.Content.ReadAsStringAsync());
            CollectionAssert.AreEqual(expectedItems, todoList);
        }
    }
}