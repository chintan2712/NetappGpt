using System.ComponentModel.DataAnnotations;

namespace NetappGpt.Models;

public class CreateSvmRequest
{
    [Required]
    public string Name { get; set; } = default!;

    public string Ipspace { get; set; } = "Default";  // optional with default
}
