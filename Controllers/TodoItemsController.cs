using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNetCore5TodoAPI.Entities;
using ASPNetCore5TodoAPI.DTOs;
using ASPNetCore5TodoAPI.Repositories;

namespace ASPNetCore5TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsRepository _todoItemsRepository;

        public TodoItemsController(ITodoItemsRepository todoItemsRepository)
        {
            _todoItemsRepository = todoItemsRepository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            List<TodoItem> storedItems = _todoItemsRepository.Get();

            List<TodoItemDTO> purchaseItems = storedItems.Select(item => new TodoItemDTO()
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            }).ToList();

            return Ok(purchaseItems);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TodoItemDTO>> GetTodoItem(string id)
        {
            var storedItem = _todoItemsRepository.Get(id);

            if (storedItem == null)
                return NotFound();

            TodoItemDTO purchaseItem = new TodoItemDTO()
            {
                Id = storedItem.Id,
                Name = storedItem.Name,
                IsComplete = storedItem.IsComplete
            };

            return Ok(purchaseItem);
        }

        // POST: api/TodoItems
        [HttpPost]
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
