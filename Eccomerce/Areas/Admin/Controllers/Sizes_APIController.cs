using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eccomerce.Areas.Admin.Data;
using Eccomerce.Areas.Admin.Models;

namespace Eccomerce.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Sizes_APIController : ControllerBase
    {
        private readonly DPContext _context;

        public Sizes_APIController(DPContext context)
        {
            _context = context;
        }

        // GET: api/Sizes_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> Getsize()
        {
            return await _context.size.ToListAsync();
        }

        // GET: api/Sizes_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Size>> GetSize(int id)
        {
            var size = await _context.size.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return size;
        }

        // PUT: api/Sizes_API/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<String> PutSize(int id, Size size)
        {
            if (id != size.Id)
            {
                return "0";
            }

            _context.Entry(size).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizeExists(id))
                {
                    return "0";
                }
                else
                {
                    throw;
                }
            }

            return "{\"id\":" + id + ",\" stt\":\"" + size.Name + "\"}";
        }

        // POST: api/Sizes_API
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Size>> PostSize(Size size)
        {
            _context.size.Add(size);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSize", new { id = size.Id }, size);
        }

        // DELETE: api/Sizes_API/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Size>> DeleteSize(int id)
        {
            var size = await _context.size.FindAsync(id);
            if (size == null)
            {
                return NotFound();
            }

            _context.size.Remove(size);
            await _context.SaveChangesAsync();

            return size;
        }

        private bool SizeExists(int id)
        {
            return _context.size.Any(e => e.Id == id);
        }
    }
}
