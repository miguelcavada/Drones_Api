
namespace Drones_Api.Data
{
    public class Drone
    {
        public int Id { get; set; }

        public string? SerialNumber { get; set; }

        public string? Model { get; set; }

        public int WeightLimit { get; set; }

        public int BatteryCapacity { get; set; }

        public string? State { get; set; }

        public List<Medication>? Medications { get; set; }
    }
}
