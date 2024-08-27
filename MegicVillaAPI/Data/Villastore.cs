using MegicVillaAPI.Models.dto;

namespace MegicVillaAPI.Data
{
    public static class Villastore
    {
        public static List<VillaDTO> VillsList = new List<VillaDTO>
            {
                new VillaDTO
                {
                    Id = 1,
                    Name="Pool Villa",
                    Sqft = 200,
                    Occupancy = 4
                },
                new VillaDTO
                {
                    Id= 2,
                    Name="Beach Villa",
                    Sqft = 300,
                    Occupancy = 4
                },
                new VillaDTO
                {
                    Id=3,
                    Name="ICE VILLA",
                    Sqft = 100,
                    Occupancy = 2
                }
            };

    }
}
