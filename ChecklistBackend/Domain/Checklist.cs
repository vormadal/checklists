namespace Domain;

public class Checklist
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public string Title { get; set; }

    public ICollection<ChecklistItem> Items { get; set; }

    public bool IsComplete => Items.All(item => item.IsComplete);

    public ChecklistType Type { get; set; }

    public int? TemplateId { get; set; }

    public Checklist? Template { get; set; }
}
