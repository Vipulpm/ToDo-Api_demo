using ToDo_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_Api.Data;

namespace ToDo_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TodoController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemData Data)
        {
            if (ModelState.IsValid)
            {
                await _context.Items.AddAsync(Data);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetItem", new { Data.Id }, Data);
            }

            return new JsonResult("Something Went Wrong") { StatusCode = 500 };
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemData item)
        {
            if(id != item.Id)
                return BadRequest();
                var existItem = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (existItem == null)
                return NotFound();

            existItem.Name = item.Name;
            existItem.Description = item.Description;
            existItem.Done = item.Done;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult>Deleteitem(int id, ItemData item)
        {
            var existItem = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if(existItem == null)
                return NotFound();

            _context.Items.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}
