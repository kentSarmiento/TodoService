using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNetCore5TodoAPI.Entities;
using ASPNetCore5TodoAPI.DTOs;
using ASPNetCore5TodoAPI.Repositories;

namespace ASPNetCore5TodoAPI.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsRepository _todoItemsRepository;

        public TodoItemsController(ITodoItemsRepository todoItemsRepository)
        {
            _todoItemsRepository = todoItemsRepository;
        }

        /// <summary>
        /// Retrieves list of Todo Items
        /// </summary>
        /// <returns>List of Todo Items</returns>
        /// <response code="200">Returns list of todo items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            List<TodoItem> storedItems = _todoItemsRepository.Get();

            List<TodoItemDTO> todoItems = storedItems.Select(item => new TodoItemDTO()
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            }).ToList();

            return Ok(todoItems);
        }

        /// <summary>
        /// Retrieves a Todo Item based on input Id
        /// </summary>
        /// <returns>Todo Item</returns>
        /// <response code="200">Returns todo item</response>
        /// <response code="404">If item with specified Id does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TodoItemDTO>> GetTodoItem(string id)
        {
            var storedItem = _todoItemsRepository.Get(id);

            if (storedItem == null)
                return NotFound();

            TodoItemDTO todoItem = new TodoItemDTO()
            {
                Id = storedItem.Id,
                Name = storedItem.Name,
                IsComplete = storedItem.IsComplete
            };

            return Ok(todoItem);
        }

        /// <summary>
        /// Registers a Todo Item
        /// </summary>
        /// <returns>Todo Item data</returns>
        /// <response code="201">If item is registered</response>
        /// <response code="400">If registration info is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Id = todoItemDTO.Id,
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _todoItemsRepository.Create(todoItem);
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                todoItemDTO);
        }

    }
}
