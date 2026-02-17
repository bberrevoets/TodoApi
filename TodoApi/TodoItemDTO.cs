public class TodoItemDTO
{
    public TodoItemDTO()
    {
    }

    public TodoItemDTO(Todo todoItem)
    {
        (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}