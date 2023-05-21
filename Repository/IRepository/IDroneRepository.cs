using Drones_Api.Data;
using Drones_Api.Models;
using System.Linq.Expressions;

namespace Drones_Api.Repository.IRepository
{
    public interface IDroneRepository
    {
        public Task<IList<DroneDTO>> GetAll(
            Expression<Func<Drone, bool>>? expression = null,
            Func<IQueryable<Drone>, IOrderedQueryable<Drone>>? orderby = null,
            List<string>? includes = null
        );
        public Task<DroneDTO> Get(Expression<Func<Drone, bool>> expression, List<string>? includes = null);
        public Task<DroneDTO> Insert(Drone entity);
        public Task<DroneDTO> Update(Drone entity);
    }
}
