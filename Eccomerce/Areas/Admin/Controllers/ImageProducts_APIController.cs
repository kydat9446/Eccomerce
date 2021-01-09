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
    public class ImageProducts_APIController : ControllerBase
    {
        private readonly DPContext _context;

        public ImageProducts_APIController(DPContext context)
        {
            _context = context;
        }
        public class ImageP_Update_STT
        {
            public int id { get; set; }
            public bool stt { get; set; }
        }
        [HttpPost]
        public string UpdateStatus(ImageP_Update_STT req)
        {
            (from p in _context.imageProduct where p.Id == req.id select p).ToList().ForEach(x => x.Status = req.stt);
            _context.SaveChanges();
            return "{\"id\":" + req.id + ",\"stt\":\"" + req.stt + "\"}";
        }
        // GET: api/ImageProducts_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageProduct>>> GetimageProduct()
        {
            return await _context.imageProduct.ToListAsync();
        }

        // GET: api/ImageProducts_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageProduct>> GetImageProduct(int id)
        {
            var imageProduct = await _context.imageProduct.FindAsync(id);

            if (imageProduct == null)
            {
                return NotFound();
            }

            return imageProduct;
        }

        // PUT: api/ImageProducts_API/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageProduct(int id, ImageProduct imageProduct)
        {
            if (id != imageProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(imageProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageProductExists(id))
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

        // POST: api/ImageProducts_API
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ImageProduct>> PostImageProduct(ImageProduct imageProduct)
        {
            _context.imageProduct.Add(imageProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageProduct", new { id = imageProduct.Id }, imageProduct);
        }

        // DELETE: api/ImageProducts_API/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ImageProduct>> DeleteImageProduct(int id)
        {
            var imageProduct = await _context.imageProduct.FindAsync(id);
            if (imageProduct == null)
            {
                return NotFound();
            }

            _context.imageProduct.Remove(imageProduct);
            await _context.SaveChangesAsync();

            return imageProduct;
        }

        private bool ImageProductExists(int id)
        {
            return _context.imageProduct.Any(e => e.Id == id);
        }
    }
}
