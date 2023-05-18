using AutoMapper;
using Drones_Api.Data;
using Drones_Api.Models;
using Drones_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Drones_Api.Repository
{
    public class DroneRepository : IDroneRepository
    {
        private readonly DronesDB _context;
        private readonly IMapper _mapper;

        public DroneRepository(DronesDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DroneDTO> Get(Expression<Func<Drone, bool>> expression, List<string>? includes = null)
        {
            var query = _context.Drones.AsQueryable();
            if (includes != null)
            {
                foreach (var includeProp in includes)
                {
                    query = query.Include(includeProp);
                }
            }
            var result = await query.AsNoTracking().FirstOrDefaultAsync(expression);
            return _mapper.Map<DroneDTO>(result);
        }

        public async Task<DroneDTO> Insert(CreateDroneDTO droneDTO)
        {
            try
            {
                var newDrone = _mapper.Map<Drone>(droneDTO);
                await _context.Drones.AddAsync(newDrone);
                await _context.SaveChangesAsync();
                return _mapper.Map<DroneDTO>(newDrone);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
