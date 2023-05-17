using AutoMapper;
using Drones_Api.Data;
using Drones_Api.Models;
using Drones_Api.Repository.IRepository;

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

        public async Task<DroneDTO> GetById(int id)
        {
            try
            {
                var drone = await _context.Drones.FindAsync(id);
                var result = _mapper.Map<DroneDTO>(drone);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DroneDTO> Insert(CreateDroneDTO droneDTO)
        {
            try
            {
                var newDrone = _mapper.Map<Drone>(droneDTO);
                await _context.Drones.AddAsync(newDrone);
                await _context.SaveChangesAsync();
                var result = _mapper.Map<DroneDTO>(newDrone);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
