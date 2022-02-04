using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetTodoAPI.DTOs
{
    public class TodoItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class CreateTodoItemDTO
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class GetTodoItemDTO : TodoItemDTO {}
    public class UpdateTodoItemDTO : TodoItemDTO {}
}
