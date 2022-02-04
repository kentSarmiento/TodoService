using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetTodoAPI.Datastores
{
    public class TodoItemsDatabaseSettings : ITodoItemsDatabaseSettings
    {
        public string TodoItemsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
