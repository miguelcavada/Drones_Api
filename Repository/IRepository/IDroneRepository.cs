using Drones_Api.Data;
using Drones_Api.Models;

namespace Drones_Api.Repository.IRepository
{
    public interface IDroneRepository
    {
        public Task<DroneDTO> GetById(int id);
        public Task<DroneDTO> Insert(CreateDroneDTO droneDTO);
    }
}
