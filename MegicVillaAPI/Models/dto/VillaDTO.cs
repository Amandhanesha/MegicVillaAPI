﻿using System.ComponentModel.DataAnnotations;

namespace MegicVillaAPI.Models.dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        public int Sqft { get; set; }

        public int Occupancy { get; set; }

    }
}
