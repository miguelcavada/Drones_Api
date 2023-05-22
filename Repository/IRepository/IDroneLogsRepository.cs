using Drones_Api.Data;
using Drones_Api.Models;
using System.Linq.Expressions;

namespace Drones_Api.Repository.IRepository
{
    public interface IDroneLogsRepository
    {
        public Task<IList<DroneLogsDTO>> GetAll(
            Expression<Func<DroneLogs, bool>>? expression = null,
            Func<IQueryable<DroneLogs>, IOrderedQueryable<DroneLogs>>? orderby = null,
            List<string>? includes = null
        );
        public Task InsertRange(IEnumerable<DroneLogs> entities);
    }
}
