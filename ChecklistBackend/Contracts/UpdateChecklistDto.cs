using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class UpdateChecklistDto
{
    [Required]
    public string Title { get; set; }
    
}
