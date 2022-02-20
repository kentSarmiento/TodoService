namespace ASPNetTodoService.Domain.Entities
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public string Secret { get; set; }
    }
}
