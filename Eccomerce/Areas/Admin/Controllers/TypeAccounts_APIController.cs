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
    public class TypeAccounts_APIController : ControllerBase
    {
        private readonly DPContext _context;

        public TypeAccounts_APIController(DPContext context)
        {
            _context = context;
        }

        // GET: api/TypeAccounts_API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeAccount>>> GettypeAccount()
        {
            return await _context.typeAccount.ToListAsync();
        }

        // GET: api/TypeAccounts_API/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeAccount>> GetTypeAccount(int id)
        {
            var typeAccount = await _context.typeAccount.FindAsync(id);

            if (typeAccount == null)
            {
                return NotFound();
            }

            return typeAccount;
        }

        // PUT: api/TypeAccounts_API/PutTypeAccount/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<String> PutTypeAccount(int id, TypeAccount typeAccount)
        {
            if (id != typeAccount.Id)
            {
                return "0";
            }

            _context.Entry(typeAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeAccountExists(id))
                {
                    return "0";
                }
                else
                {
                    throw;
                }
            }

            return "{\"id\":" +id+ ",\" stt\":\""+typeAccount.Status+"\"}";
        }

        // POST: api/TypeAccounts_API
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TypeAccount>> PostTypeAccount(TypeAccount typeAccount)
        {
            _context.typeAccount.Add(typeAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeAccount", new { id = typeAccount.Id }, typeAccount);
        }

        // DELETE: api/TypeAccounts_API/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeAccount>> DeleteTypeAccount(int id)
        {
            var typeAccount = await _context.typeAccount.FindAsync(id);
            if (typeAccount == null)
            {
                return NotFound();
            }

            _context.typeAccount.Remove(typeAccount);
            await _context.SaveChangesAsync();

            return typeAccount;
        }

        private bool TypeAccountExists(int id)
        {
            return _context.typeAccount.Any(e => e.Id == id);
        }
    }
}
