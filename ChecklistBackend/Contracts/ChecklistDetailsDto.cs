using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class ChecklistDetailsDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime ModifiedOn { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public ICollection<ChecklistItemDto> Items { get; set; }

    [Required]
    public bool IsComplete { get; set; }

    [Required]
    public ChecklistType Type { get; set; }
}
