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

        public DroneController(IDroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }

        [HttpGet("{id:int}", Name = "GetDrone")]
        public async Task<IActionResult> GetDrone(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            try
            {
                var result = await _droneRepository.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDrone([FromBody] CreateDroneDTO droneDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _droneRepository.Insert(droneDTO);
                return CreatedAtRoute(nameof(GetDrone), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }
    }
}
