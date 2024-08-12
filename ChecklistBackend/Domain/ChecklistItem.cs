namespace Domain;

public class ChecklistItem
{
    public int Id { get; set; }

    public int Order { get; set; }

    public string Title { get; set; }

    public bool IsComplete { get; set; }

    public int ChecklistId { get; set; }

    public Checklist Checklist { get; set; }

    public int? CopiedFromId { get; set; }

    public ChecklistItem? CopiedFrom { get; set; }

}
