using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiBookingAPI.Data;
using SkiBookingAPI.Data.Models;

namespace SkiBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiftTicketsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LiftTicketsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/LiftTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiftTicket>>> GetLiftTickets()
        {
            if (_context.LiftTicket == null)
            {
                return NotFound();
            }
            return await _context.LiftTicket.Where(item => item.Active).ToListAsync();
        }

        // GET: api/LiftTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LiftTicket>> GetLiftTicket(long id)
        {
            if (_context.LiftTicket == null)
            {
                return NotFound();
            }
            var liftTicket = await _context.LiftTicket.FindAsync(id);

            if (liftTicket == null)
            {
                return NotFound();
            }

            return liftTicket;
        }

        // PUT: api/LiftTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiftTicket(long id, LiftTicket liftTicket)
        {
            if (id != liftTicket.LiftTicketID)
            {
                return BadRequest();
            }

            _context.Entry(liftTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiftTicketExists(id))
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

        // POST: api/LiftTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LiftTicket>> PostLiftTicket(LiftTicket liftTicket)
        {
          if (_context.LiftTicket == null)
          {
              return Problem("Entity set 'ApplicationDBContext.LiftTickets'  is null.");
          }
            _context.LiftTicket.Add(liftTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLiftTicket", new { id = liftTicket.LiftTicketID }, liftTicket);
        }

        // DELETE: api/LiftTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLiftTicket(long id)
        {
            if (_context.LiftTicket == null)
            {
                return NotFound();
            }
            var liftTicket = await _context.LiftTicket.FindAsync(id);
            if (liftTicket == null)
            {
                return NotFound();
            }

            _context.LiftTicket.Remove(liftTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LiftTicketExists(long id)
        {
            return (_context.LiftTicket?.Any(e => e.LiftTicketID == id)).GetValueOrDefault();
        }
    }
}
