namespace Drones_Api.Models
{
    public class DroneLogsDTO
    {
        public int Id { get; set; }
        public int DroneId { get; set; }
        public int BatteryLevel { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
