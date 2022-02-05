namespace ASPNetTodoService.Infrastructure.Datastores
{
    public class TodoItemsDatabaseSettings : ITodoItemsDatabaseSettings
    {
        public string TodoItemsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
