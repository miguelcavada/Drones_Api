using Drones_Api.Data;
using Drones_Api.Models;
using System.Linq.Expressions;

namespace Drones_Api.Repository.IRepository
{
    public interface IDroneRepository
    {
        public Task<DroneDTO> Get(Expression<Func<Drone, bool>> expression, List<string>? includes = null);
        public Task<DroneDTO> Insert(CreateDroneDTO droneDTO);
    }
}
