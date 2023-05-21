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
            CreateMap<Drone, CreateDroneDTO>().ReverseMap();
            CreateMap<Drone, UpdatedDroneDTO>().ReverseMap();
            CreateMap<Medication, MedicationDTO>().ReverseMap();
            CreateMap<Medication, CreateMedicationDTO>().ReverseMap();
        }
    }
}
