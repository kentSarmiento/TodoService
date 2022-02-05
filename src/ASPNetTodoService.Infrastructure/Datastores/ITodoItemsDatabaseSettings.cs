namespace ASPNetTodoService.Infrastructure.Datastores
{
    public interface ITodoItemsDatabaseSettings
    {
        string TodoItemsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
