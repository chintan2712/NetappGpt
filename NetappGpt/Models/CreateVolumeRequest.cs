using System.ComponentModel.DataAnnotations;

namespace NetappGpt.Models
{
    public class CreateVolumeRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Size { get; set; } = default!;

        [Required]
        public Svm Svm { get; set; } = default!;
    }

    public class Svm
    {
        public required string Name { get; set; }
    }

}
