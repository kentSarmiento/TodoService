using System;

namespace ASPNetTodoService.API.DTOs
{
    public class TodoItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public override bool Equals(object obj)
        {
            TodoItemDTO todoItem = obj as TodoItemDTO;
            return todoItem != null &&
                Object.Equals(this.Id, todoItem.Id) &&
                Object.Equals(this.Name, todoItem.Name) &&
                Object.Equals(this.IsComplete, todoItem.IsComplete);
        }
    }

    public class CreateTodoItemDTO
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class GetTodoItemDTO : TodoItemDTO {}
    public class UpdateTodoItemDTO : TodoItemDTO {}
}
