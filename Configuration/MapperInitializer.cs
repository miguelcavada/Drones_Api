using AutoMapper;
using Drones_Api.Data;
using Drones_Api.Models;

namespace Drones_Api.Configuration
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Drone, DroneDTO>().ReverseMap();
            CreateMap<Medication, MedicationDTO>().ReverseMap();
        }
    }
}
