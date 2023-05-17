﻿using System.ComponentModel.DataAnnotations;

namespace Drones_Api.Models
{
    public class CreateDroneDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for the SerialNumber is 100 characters.")]
        public string? SerialNumber { get; set; }

        [Required]
        public string? Model { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the WeightLimit is 500 g (grame).")]
        public int WeightLimit { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length for the BatteryCapacity is 100 % (percent).")]
        public int BatteryCapacity { get; set; }

        public string? State { get; set; }
    }
}
