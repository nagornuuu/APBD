using Exercise4.Models;
using Exercise4.Models.DTOs;
using Exercise4.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Exercise4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalsController(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAnimals(string orderBy = "name")
        {
            var animals = await _animalsRepository.GetAnimalsAsync(orderBy);
            return Ok(animals);
        }
        [HttpPost]
        public async Task<IActionResult> AddAnimal(Animal animal)
        {
            var existingAnimal = await _animalsRepository.GetAnimalByIdAsync(animal.ID);
            if (existingAnimal != null)
            {
                return Conflict(); //HTTP 409
            }
            await _animalsRepository.AddAnimalAsync(animal);
            
            if (animal == null)
            {
                return BadRequest(); //HTTP 400
            }

            return CreatedAtAction(nameof(GetAnimals), new { id = animal.ID }, animal);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animal)
        {
            if (id != animal.ID)
            {
                return NotFound(); //HTTP 404
            }

            var existingAnimal = await _animalsRepository.GetAnimalByIdAsync(id);
            if (existingAnimal == null)
            {
                return BadRequest(); //HTTP 400
            }

            await _animalsRepository.UpdateAnimalAsync(animal);
            return Ok(animal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _animalsRepository.GetAnimalByIdAsync(id);

            if (animal == null)
            {
                return NotFound(); // HTTP 404
            }

            await _animalsRepository.DeleteAnimalAsync(animal.ID);
            await _animalsRepository.UpdateAnimalAsync(animal);
            return Ok();
        }
    }
}




