using AutoMapper;
using Drones_Api.Data;
using Drones_Api.Models;
using Drones_Api.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drones_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly IDroneRepository _droneRepository;
        private readonly IMapper _mapper;

        public DroneController(IDroneRepository droneRepository, IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "GetDrone")]
        public async Task<IActionResult> GetDrone(int id)
        {
            try
            {
                var result = await _droneRepository.Get(x => x.Id == id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDrone([FromBody] CreateDroneDTO droneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _droneRepository.Insert(droneDto);
                return CreatedAtRoute(nameof(GetDrone), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }

        [HttpGet("BatteryLevel/{id:int}", Name = "GetBatteryLevel")]
        public async Task<IActionResult> GetBatteryLevel(int id)
        {
            try
            {
                var result = await _droneRepository.Get(x => x.Id == id);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(new { data = $"The battery level for given drone is {result.BatteryCapacity}%" }.data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }

        [HttpGet("Charge/{id:int}", Name = "GetCharge")]
        public async Task<IActionResult> GetCharge(int id)
        {
            try
            {
                var result = await _droneRepository.Get(x =>x.Id == id, new List<string> { "Medications" });

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }
    }
}
