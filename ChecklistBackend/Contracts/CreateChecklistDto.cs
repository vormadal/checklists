using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class CreateChecklistDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public ChecklistType Type { get; set; }

    
}
