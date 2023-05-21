namespace Drones_Api.Models
{
    public class UpdatedDroneDTO : CreateDroneDTO
    {
        public IList<CreateMedicationDTO>? Medications { get; set; }
    }
}
