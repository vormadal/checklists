using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class UpdateChecklistItemDto
{
    [Required]
    public int Order { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public bool IsComplete { get; set; }
}
