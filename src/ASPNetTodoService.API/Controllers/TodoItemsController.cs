using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using ASPNetTodoService.API.DTOs;
using ASPNetTodoService.Domain.Entities;
using ASPNetTodoService.Domain.Interfaces;

namespace ASPNetTodoService.API.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsRepository _todoItemsRepository;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemsRepository todoItemsRepository, IMapper mapper)
        {
            _todoItemsRepository = todoItemsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves list of Todo Items
        /// </summary>
        /// <returns>List of Todo Items</returns>
        /// <response code="200">Returns list of todo items</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User is not authorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<GetTodoItemDTO>>> GetTodoItems()
        {
            List<TodoItem> storedItems = await _todoItemsRepository.GetAsync();
            List<GetTodoItemDTO> todoItems = storedItems.Select(
                item => _mapper.Map<GetTodoItemDTO>(item)).ToList();

            return Ok(todoItems);
        }

        /// <summary>
        /// Retrieves a Todo Item based on input Id
        /// </summary>
        /// <returns>Todo Item</returns>
        /// <response code="200">Returns todo item</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User is not authorized</response>
        /// <response code="404">If item with specified Id does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetTodoItemDTO>>> GetTodoItem(string id)
        {
            var storedItem = await _todoItemsRepository.GetAsync(id);

            if (storedItem == null)
                return NotFound();

            GetTodoItemDTO todoItem = _mapper.Map<GetTodoItemDTO>(storedItem);
            return Ok(todoItem);
        }

        /// <summary>
        /// Registers a Todo Item
        /// </summary>
        /// <returns>Todo Item data</returns>
        /// <response code="201">If item is registered</response>
        /// <response code="400">If registration info is invalid</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User is not authorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(CreateTodoItemDTO todoItemDto)
        {
            TodoItem todoItem = _mapper.Map<TodoItem>(todoItemDto);
            await _todoItemsRepository.CreateAsync(todoItem);

            TodoItemDTO createdItemDto = _mapper.Map<TodoItemDTO>(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, createdItemDto);
        }

        /// <summary>
        /// Updates a Todo Item based on input Id and data (using PUT method)
        /// </summary>
        /// <returns>None</returns>
        /// <response code="204">Todo item is updated</response>
        /// <response code="400">Request contains invalid data</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User is not authorized</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateTodoItem(string id, TodoItemDTO todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            TodoItem todoItem = _mapper.Map<TodoItem>(todoItemDto);
            await _todoItemsRepository.UpdateAsync(id, todoItem);

            return NoContent();
        }

        /// <summary>
        /// Updates a Todo Item based on input Id and data (using PATCH method)
        /// </summary>
        /// <returns>None</returns>
        /// <response code="204">Todo item is updated</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User is not authorized</response>
        /// <response code="404">If item with specified Id does not exist</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PatchTodoItem(string id, [FromBody] JsonPatchDocument<TodoItemDTO> patchDoc)
        {
            var todoItem = await _todoItemsRepository.GetAsync(id);

            if (todoItem == null)
                return NotFound();

            TodoItemDTO todoItemDto = _mapper.Map<TodoItemDTO>(todoItem);
            patchDoc.ApplyTo(todoItemDto);

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;
            await _todoItemsRepository.UpdateAsync(id, todoItem);

            return NoContent();
        }

        /// <summary>
        /// Deletes a Todo Item based on input Id
        /// </summary>
        /// <returns>None</returns>
        /// <response code="204">Todo item is deleted</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User is not authorized</response>
        /// <response code="404">If item with specified Id does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTodoItem(string id)
        {
            var storedItem = await _todoItemsRepository.GetAsync(id);

            if (storedItem == null)
                return NotFound();

            await _todoItemsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
