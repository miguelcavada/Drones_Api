using System.ComponentModel.DataAnnotations;

namespace Drones_Api.Models
{
    public class DroneDTO : CreateDroneDTO
    {
        public int Id { get; set; }
        public IList<CreateMedicationDTO>? Medications { get; set; }
    }
}
