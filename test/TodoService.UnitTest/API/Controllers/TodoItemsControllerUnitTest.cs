using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TodoService.API.Controllers;
using TodoService.API.DTOs;
using TodoService.Domain.Entities;
using TodoService.Domain.Interfaces;
using System.Threading.Tasks;

namespace TodoService.UnitTest.API.Controllers
{
    [TestFixture]
    public class TodoItemsControllerUnitTest
    {
        private readonly string TODO_ITEM_ID = "1234";
        private readonly string TODO_ITEM_NAME = "NUnit + Moq Testing";
        private readonly string TODO_ITEM_SECRET = "4321";

        [SetUp]
        public void Setup() { }

        [Test]
        public async Task GetTodoItems_NoRegisteredTodoItems_ReturnsEmptyList()
        {
            var repoItems = new List<TodoItem>();
            var expectedItems = new List<GetTodoItemDTO>();

            var repoMock = new Mock<ITodoItemsRepository>();
            var mapperMock = new Mock<IMapper>();

            repoMock.Setup(r => r.GetAsync()).ReturnsAsync(repoItems);

            var todoItemsController = new TodoItemsController(repoMock.Object, mapperMock.Object);
            var response = await todoItemsController.GetTodoItems();

            Assert.IsInstanceOf<ActionResult<IEnumerable<GetTodoItemDTO>>>(response);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);

            var responseItems = (response.Result as OkObjectResult).Value as IEnumerable<GetTodoItemDTO>;
            CollectionAssert.AreEqual(expectedItems, responseItems);

            repoMock.Verify(r => r.GetAsync(), Times.Once());
            mapperMock.Verify(m => m.Map<GetTodoItemDTO>(It.IsAny<TodoItem>()), Times.Never());
        }

        [Test]
        public async Task GetTodoItems_WithRegisteredTodoItems_ReturnsNonEmptyList()
        {
            var todoItem = new TodoItem() { Id = TODO_ITEM_ID, TaskName = TODO_ITEM_NAME, Done = true, Secret = TODO_ITEM_SECRET };
            var todoItemDto = new GetTodoItemDTO() { Id = TODO_ITEM_ID, TaskName = TODO_ITEM_NAME, Done = true };

            var repoItems = new List<TodoItem>() { todoItem };
            var expectedItems = new List<GetTodoItemDTO>() { todoItemDto };

            var repoMock = new Mock<ITodoItemsRepository>();
            var mapperMock = new Mock<IMapper>();

            repoMock.Setup(r => r.GetAsync()).ReturnsAsync(repoItems);
            mapperMock.Setup(m => m.Map<GetTodoItemDTO>(It.IsAny<TodoItem>())).Returns(todoItemDto);

            var todoItemsController = new TodoItemsController(repoMock.Object, mapperMock.Object);
            var response = await todoItemsController.GetTodoItems();

            Assert.IsInstanceOf<ActionResult<IEnumerable<GetTodoItemDTO>>>(response);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);

            var responseItems = (response.Result as OkObjectResult).Value as IEnumerable<GetTodoItemDTO>;
            CollectionAssert.AreEqual(expectedItems, responseItems);

            repoMock.Verify(r => r.GetAsync(), Times.Once());
            mapperMock.Verify(m => m.Map<GetTodoItemDTO>(todoItem), Times.Once());
        }
    }
}