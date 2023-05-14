using System.ComponentModel.DataAnnotations;

namespace Drones_Api.Models
{
    public class MedicationDTO
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[A-Z0-9_]+$")]
        public string? Code { get; set; }

        public string? Image { get; set; }
    }
}
