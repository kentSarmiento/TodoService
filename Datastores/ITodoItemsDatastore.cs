﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCore5TodoAPI.Entities;

namespace ASPNetCore5TodoAPI.Datastores
{
    public interface ITodoItemsDatastore
    {
        public List<TodoItem> Get();

        public TodoItem Get(string id);

        public TodoItem Create(TodoItem item);

        public void Update(string id, TodoItem item);

        public void Delete(string id);
    }
}