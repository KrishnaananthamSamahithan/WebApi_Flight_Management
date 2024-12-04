using WebApi.Data;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightManagementAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Secures all endpoints in this controller
    public class FlightsController : ControllerBase
    {
        private readonly FlightManagementDbContext _context;

        public FlightsController(FlightManagementDbContext context)
        {
            _context = context;
        }
        //Get request to get all the details  
        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetAllFlights()
        {
            return Ok(await _context.Flights.ToListAsync());
        }

        //Get flights by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound("Flight not found.");
            return Ok(flight);
        }

        //Post method for create a fright
        [HttpPost]
        public async Task<ActionResult> AddFlight(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Id }, flight);
        }

        //Put method for update flight by id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlight(int id, Flight flight)
        {
            if (id != flight.Id) return BadRequest("Flight ID mismatch.");

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Flights.AnyAsync(e => e.Id == id)) return NotFound("Flight not found.");
                throw;
            }

            return NoContent();
        }

        //Delete method to delete flights by id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound("Flight not found.");

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
