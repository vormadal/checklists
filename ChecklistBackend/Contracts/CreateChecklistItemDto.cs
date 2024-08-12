using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class CreateChecklistItemDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public bool IsComplete { get; set; }

    [Required]
    public int Order { get; set; }
}
