using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetTodoService.Infrastructure.Datastores
{
    public interface ITodoItemsDatabaseSettings
    {
        string TodoItemsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
