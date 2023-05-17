using System.ComponentModel.DataAnnotations;

namespace Drones_Api.Models
{
    public class MedicationDTO : CreateMedicationDTO
    {
        public int Id { get; set; }
        public int? DroneId { get; set; }
    }
}
