using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class ChecklistItemDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int Order { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public bool IsComplete { get; set; }

    [Required]
    public int ChecklistId { get; set; }
}
