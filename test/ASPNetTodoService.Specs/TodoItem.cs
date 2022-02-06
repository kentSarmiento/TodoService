using System;

namespace ASPNetTodoService.Specs
{
    public class TodoItem
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public override bool Equals(object obj)
        {
            TodoItem? todoItem = obj as TodoItem;
            return todoItem != null &&
                Object.Equals(this.Name, todoItem.Name) &&
                Object.Equals(this.IsComplete, todoItem.IsComplete);
        }
    }
}
