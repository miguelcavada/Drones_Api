using AutoMapper;
using Drones_Api.Data;
using Drones_Api.Models;
using Drones_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Drones_Api.Repository
{
    public class DroneLogsRepository : IDroneLogsRepository
    {
        private readonly DronesDB _db;
        private readonly IMapper _mapper;

        public DroneLogsRepository(DronesDB db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IList<DroneLogsDTO>> GetAll(
            Expression<Func<DroneLogs, bool>>? expression = null,
            Func<IQueryable<DroneLogs>, IOrderedQueryable<DroneLogs>>? orderby = null,
            List<string>? includes = null
            )
        {
            var query = _db.DroneLogs.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderby != null)
            {
                query = orderby(query);
            }
            var result = await query.AsNoTracking().ToListAsync();
            return _mapper.Map<IList<DroneLogsDTO>>(result);
        }

        public async Task InsertRange(IEnumerable<DroneLogs> entities)
        {
            try
            {
                await _db.AddRangeAsync(entities);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
