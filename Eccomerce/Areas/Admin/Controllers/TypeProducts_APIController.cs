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
    public class TypeProducts_APIController : ControllerBase
    {
        private readonly DPContext _context;

        public TypeProducts_APIController(DPContext context)
        {
            _context = context;
        }

        // GET: api/TypeProducts_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduct>>> GettypeProduct()
        {
            return await _context.typeProduct.ToListAsync();
        }

        // GET: api/TypeProducts_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduct>> GetTypeProduct(int id)
        {
            var typeProduct = await _context.typeProduct.FindAsync(id);

            if (typeProduct == null)
            {
                return NotFound();
            }

            return typeProduct;
        }

        // PUT: api/TypeProducts_API/PutTypeProduct/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<string> PutTypeProduct(int id, TypeProduct typeProduct)
        {
            if (id != typeProduct.Id)
            {
                return "0";
            }

            _context.Entry(typeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeProductExists(id))
                {
                    return "0";
                }
                else
                {
                    throw;
                }
            }

            return "{\"id\":" + id + ",\" stt\":\""+ typeProduct.Status + "\"}";
            
        }

        // POST: api/TypeProducts_API
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TypeProduct>> PostTypeProduct(TypeProduct typeProduct)
        {
            _context.typeProduct.Add(typeProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeProduct", new { id = typeProduct.Id }, typeProduct);
        }

        // DELETE: api/TypeProducts_API/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeProduct>> DeleteTypeProduct(int id)
        {
            var typeProduct = await _context.typeProduct.FindAsync(id);
            if (typeProduct == null)
            {
                return NotFound();
            }

            _context.typeProduct.Remove(typeProduct);
            await _context.SaveChangesAsync();

            return typeProduct;
        }

        private bool TypeProductExists(int id)
        {
            return _context.typeProduct.Any(e => e.Id == id);
        }
    }
}
