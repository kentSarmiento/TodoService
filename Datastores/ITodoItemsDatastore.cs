using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCore5TodoAPI.Models;

namespace ASPNetCore5TodoAPI.Datastores
{
    public interface ITodoItemsDatastore
    {
        public List<TodoItem> Get();

        public TodoItem Get(string id);

        public TodoItem Create(TodoItem item);
    }
}
