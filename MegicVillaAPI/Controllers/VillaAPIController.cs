using MegicVillaAPI.Data;
using MegicVillaAPI.Models;
using MegicVillaAPI.Models.dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MegicVillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> Getvillas()
        {
            return Ok(Villastore.VillsList);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> Getvilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = Villastore.VillsList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO) {

            if (Villastore.VillsList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa is there");
                return BadRequest(ModelState);
            }
            if (villaDTO == null) {
                return BadRequest(villaDTO);

            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            villaDTO.Id = Villastore.VillsList.OrderByDescending(u => u.Id).First().Id + 1;
            Villastore.VillsList.Add(villaDTO);
            return Ok(villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeletVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteVilla(int id) { 
        if(id == 0)
            {
                return BadRequest();
            }
        var Villa = Villastore.VillsList.FirstOrDefault(u=>u.Id == id);
        if(Villa == null)
            {
                return NotFound();
            }
         Villastore.VillsList.Remove(Villa);
            return NoContent(); 
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = Villastore.VillsList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent(); 
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = Villastore.VillsList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            var villaToPatch = new VillaDTO
            {
                Id = villa.Id,
                Name = villa.Name,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy
            };

            patchDTO.ApplyTo(villaToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Update the original villa with the patched values
            villa.Name = villaToPatch.Name;
            villa.Sqft = villaToPatch.Sqft;
            villa.Occupancy = villaToPatch.Occupancy;

            return NoContent();
        }

    }
}
 