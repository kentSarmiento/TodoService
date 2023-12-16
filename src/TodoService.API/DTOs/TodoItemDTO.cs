using System;

namespace TodoService.API.DTOs
{
    public class TodoItemDTO
    {
        public string Id { get; set; }
        public string TaskName { get; set; }
        public bool Done { get; set; }

        public override bool Equals(object obj)
        {
            TodoItemDTO todoItem = obj as TodoItemDTO;
            return todoItem != null &&
                Object.Equals(this.Id, todoItem.Id) &&
                Object.Equals(this.TaskName, todoItem.TaskName) &&
                Object.Equals(this.Done, todoItem.Done);
        }
    }

    public class CreateTodoItemDTO
    {
        public string TaskName { get; set; }
        public bool Done { get; set; }
    }

    public class GetTodoItemDTO : TodoItemDTO { }
    public class UpdateTodoItemDTO : TodoItemDTO { }
}
