using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eccomerce.Areas.Admin.Data;
using Eccomerce.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eccomerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypeAccountsController : Controller
    {
        private readonly DPContext _context;

        public TypeAccountsController(DPContext context)
        {
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.ListTAccount = _context.typeAccount.ToList();
            base.OnActionExecuted(context);
        }

        // GET: Admin/TypeAccounts
        public async Task<IActionResult> Index(int? id)
        {
            TypeAccount typeAccount = null;
            if (id!=null)
            {
                typeAccount = await _context.typeAccount.FirstOrDefaultAsync(m => m.Id == id);
            }
            if (TempData["result"] != null)
            {
                ViewBag.SuccesMgs = TempData["result"];
            }
            return View(typeAccount);
        }

        // GET: Admin/TypeAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAccount = await _context.typeAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeAccount == null)
            {
                return NotFound();
            }

            return View(typeAccount);
        }

        // GET: Admin/TypeAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TypeAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] TypeAccount typeAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeAccount);
                await _context.SaveChangesAsync();
                
            }
            TempData["result"] = "Thêm mới thành công";
            return RedirectToAction("Index");
        }

        // GET: Admin/TypeAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAccount = await _context.typeAccount.FindAsync(id);
            if (typeAccount == null)
            {
                return NotFound();
            }
            return View(typeAccount);
        }

        // POST: Admin/TypeAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] TypeAccount typeAccount)
        {
            if (id != typeAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeAccountExists(typeAccount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/TypeAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAccount = await _context.typeAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeAccount == null)
            {
                return NotFound();
            }

            return View(typeAccount);
        }

        // POST: Admin/TypeAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeAccount = await _context.typeAccount.FindAsync(id);
            _context.typeAccount.Remove(typeAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeAccountExists(int id)
        {
            return _context.typeAccount.Any(e => e.Id == id);
        }
    }
}
