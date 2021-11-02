using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bSide.Models;

namespace bSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bSideCardsController : ControllerBase
    {
        private readonly TodoContext _context;

        public bSideCardsController(TodoContext context)
        {
            _context = context;
        }

        //Método GET: api/bSideCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<bSideCard>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        //Método GET: api/bSideCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<bSideCard>> GetbSideCard(long id)
        {
            var bSideCard = await _context.TodoItems.FindAsync(id);

            if (bSideCard == null)
            {
                return NotFound();
            }

            return bSideCard;
        }

        //Método PUT: api/bSideCards/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutbSideCard(long id, bSideCard bSideCard)
        {
            if (id != bSideCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(bSideCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bSideCardExists(id))
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

        //Método POST: api/bSideCards
        
        [HttpPost]
        public async Task<ActionResult<bSideCard>> PostbSideCard(bSideCard bSideCard)
        {
            _context.TodoItems.Add(bSideCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetbSideCard), new { id = bSideCard.Id}, bSideCard); // Se obtiene el valor de de la solicitud http
        }

        //Método DELETE: api/bSideCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletebSideCard(long id)
        {
            var bSideCard = await _context.TodoItems.FindAsync(id);
            if (bSideCard == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(bSideCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool bSideCardExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
