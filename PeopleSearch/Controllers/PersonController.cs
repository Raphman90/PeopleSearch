using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _context;

        public PersonController(PersonContext context)
        {
            _context = context;

            if(!_context.People.Any())
            {
                SeedPeople(_context);
            }
        }

        // GET: api/Person
        [HttpGet]
        public IActionResult GetPeople()
        {
            return Ok(_context.People);
        }

        [HttpGet("search/{searchString}")]
        public IActionResult RetrievePeople([FromRoute]string searchString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var peopleList = _context.People.Where(x => x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                      x.LastName.ToLower().Contains(searchString.ToLower()));

            if (peopleList == null)
            {
                return NotFound();
            }

            return Ok(peopleList);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> RetrievePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personModel = await _context.People.FindAsync(id);

            if (personModel == null)
            {
                return NotFound();
            }

            return Ok(personModel);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] int id, [FromBody] PersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(personModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Person
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.People.Add(personModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonModel", new { id = personModel.Id }, personModel);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personModel = await _context.People.FindAsync(id);
            if (personModel == null)
            {
                return NotFound();
            }

            _context.People.Remove(personModel);
            await _context.SaveChangesAsync();

            return Ok(personModel);
        }

        private bool PersonModelExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }


        //this is used to generate some people to search against.
        private void SeedPeople(PersonContext context)
        {
            var personOne = new PersonModel
            {
                FirstName = "Arnold",
                LastName = "Schwarzenegger",
                Age = 71,
                Address = "3110 Main Street Suite 300 Santa Monica, CA 90405 - 5354",
                Interests = "politics, body building, entreprenuership, and acting",
                ImageLocation = "/images/arnold.gif"
            };

            context.Add(personOne);

            var personTwo = new PersonModel
            {
                FirstName = "Raphael",
                LastName = "Rosa",
                Age = 28,
                Address = "123 Fake street New York, NY 10001",
                Interests = "computer hardware, video games, cryptocurrency, karaoke, and game development"
            };

            context.Add(personTwo);

            var personThree = new PersonModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                Age = 26,
                Address = "42 Wallaby Way Sydney, AUS",
                Interests = "software development, renewable energy, and space exploration"
            };

            context.Add(personThree);

            context.SaveChanges();

        }
    }
}