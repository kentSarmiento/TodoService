using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ASPNetTodoService.API.Controllers;
using ASPNetTodoService.API.DTOs;
using ASPNetTodoService.Infrastructure.Repositories;
using ASPNetTodoService.Domain.Entities;

namespace ASPNetTodoService.UnitTest.API.Controllers
{
    [TestFixture]
    public class TodoItemsControllerUnitTest
    {
        private readonly string TODO_ITEM_ID = "1234";
        private readonly string TODO_ITEM_NAME = "NUnit + Moq Testing";
        private readonly string TODO_ITEM_SECRET = "4321";

        [SetUp]
        public void Setup() {}

        [Test]
        public void GetTodoItems_NoRegisteredTodoItems_ReturnsEmptyList()
        {
            var repoItems = new List<TodoItem>();
            var expectedItems = new List<GetTodoItemDTO>();

            var repoMock = new Mock<ITodoItemsRepository>();
            var mapperMock = new Mock<IMapper>();

            repoMock.Setup(r => r.Get()).Returns(repoItems);

            var todoItemsController = new TodoItemsController(repoMock.Object, mapperMock.Object);
            var response = todoItemsController.GetTodoItems();

            Assert.IsInstanceOf<ActionResult<IEnumerable<GetTodoItemDTO>>>(response);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);

            var responseItems = (response.Result as OkObjectResult).Value as IEnumerable<GetTodoItemDTO>;
            CollectionAssert.AreEqual(expectedItems, responseItems);

            repoMock.Verify(r => r.Get(), Times.Once());
            mapperMock.Verify(m => m.Map<GetTodoItemDTO>(It.IsAny<TodoItem>()), Times.Never());
        }

        [Test]
        public void GetTodoItems_WithRegisteredTodoItems_ReturnsNonEmptyList()
        {
            var todoItem = new TodoItem() { Id = TODO_ITEM_ID, Name = TODO_ITEM_NAME, IsComplete = true, Secret = TODO_ITEM_SECRET };
            var todoItemDto = new GetTodoItemDTO() { Id = TODO_ITEM_ID, Name = TODO_ITEM_NAME, IsComplete = true };

            var repoItems = new List<TodoItem>() { todoItem };
            var expectedItems = new List<GetTodoItemDTO>() { todoItemDto };

            var repoMock = new Mock<ITodoItemsRepository>();
            var mapperMock = new Mock<IMapper>();

            repoMock.Setup(r => r.Get()).Returns(repoItems);
            mapperMock.Setup(m => m.Map<GetTodoItemDTO>(It.IsAny<TodoItem>())).Returns(todoItemDto);

            var todoItemsController = new TodoItemsController(repoMock.Object, mapperMock.Object);
            var response = todoItemsController.GetTodoItems();

            Assert.IsInstanceOf<ActionResult<IEnumerable<GetTodoItemDTO>>>(response);
            Assert.IsInstanceOf<OkObjectResult>(response.Result);

            var responseItems = (response.Result as OkObjectResult).Value as IEnumerable<GetTodoItemDTO>;
            CollectionAssert.AreEqual(expectedItems, responseItems);

            repoMock.Verify(r => r.Get(), Times.Once());
            mapperMock.Verify(m => m.Map<GetTodoItemDTO>(todoItem), Times.Once());
        }
    }
}