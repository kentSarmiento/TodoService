using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore5TodoAPI.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
