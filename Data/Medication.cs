using System.ComponentModel.DataAnnotations.Schema;

namespace Drones_Api.Data
{
    public class Medication
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Weight { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }

        [ForeignKey(nameof(DroneId))]
        public int? DroneId { get; set; }

        public Drone? Drone { get; set; }
    }
}
