using System.ComponentModel.DataAnnotations;

namespace Drones_Api.Models
{
    public class CreateMedicationDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]+$")]
        public string? Name { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        [RegularExpression("^[A-Z0-9_]+$")]
        public string? Code { get; set; }

        public string? Image { get; set; }
    }
}
