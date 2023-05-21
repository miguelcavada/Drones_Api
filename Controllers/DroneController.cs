using AutoMapper;
using Drones_Api.Common;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// checking available drones for loading
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDrones()
        {
            var result = await _droneRepository.GetAll(x => x.State.Equals(DroneState.IDLE.ToString()));
            if (!result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

        /// <summary>
        /// registering a drone
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDrone([FromBody] CreateDroneDTO droneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _droneRepository.Insert(_mapper.Map<Drone>(droneDto));
                return CreatedAtRoute(nameof(GetDrone), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// check drone battery level for a given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        [HttpGet("BatteryLevel/{id:int}", Name = "GetBatteryLevel")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// checking loaded medication items for a given drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        [HttpGet("Charge/{id:int}", Name = "GetCharge")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCharge(int id)
        {
            try
            {
                var result = await _droneRepository.Get(x => x.Id == id, new List<string> { "Medications" });

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// loading a drone with medication items
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="medicationDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoadMedications(int id, [FromBody] IEnumerable<CreateMedicationDTO> medicationDtoList)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var droneDto = await _droneRepository.Get(x => x.Id == id);
                if (droneDto == null)
                {
                    return NotFound();
                }

                if (droneDto.BatteryCapacity < 25)
                {
                    return BadRequest("The given drone has its battery level less than 25%");
                }
                else
                {
                    var updDrone = _mapper.Map<Drone>(droneDto);
                    updDrone.State = DroneState.LOADING.ToString();
                    await _droneRepository.Update(updDrone);

                    foreach (var item in _mapper.Map<IEnumerable<Medication>>(medicationDtoList))
                    {
                        var suma = updDrone.Medications?.Sum(x => x.Weight) + item.Weight;
                        if (suma > updDrone.WeightLimit)
                        {
                            break;
                        }
                        updDrone.Medications?.Add(item);
                    }
                    updDrone.State = DroneState.LOADED.ToString();
                    await _droneRepository.Update(updDrone);
                    return CreatedAtRoute(nameof(GetDrone), new { id = updDrone.Id }, updDrone);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
