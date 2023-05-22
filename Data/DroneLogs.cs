namespace Drones_Api.Data
{
    public class DroneLogs
    {
        public int Id { get; set; }
        public int DroneId { get; set; }
        public int BatteryLevel { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
