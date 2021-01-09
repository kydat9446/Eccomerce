using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eccomerce.Areas.Admin.Data;
using Eccomerce.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eccomerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageProductsController : Controller
    {
        private readonly DPContext _context;

        public ImageProductsController(DPContext context)
        {
            _context = context;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (Request.QueryString.Value.IndexOf("s_name") < 0)
            {
                ViewBag.ListImgProduct = _context.imageProduct.ToList();
            }
            base.OnActionExecuted(context);
        }
        // GET: Admin/ImageProducts
        public async Task<IActionResult> Index(int? id,string s_name,string s_stt)
        {

            ViewData["IdProduct"] = new SelectList(_context.product, "Id", "Name");

            ImageProduct imageProduct = null;
            if (id != null)
            {
                imageProduct = await _context.imageProduct.FirstOrDefaultAsync(m => m.Id == id);
            }
            if (s_name != null)
            {
                if (s_stt == null)
                {
                    ViewBag.ListImgProduct = (from p in _context.imageProduct where p.product.Name.IndexOf(s_name) >= 0 select p).ToList();
                }
                else
                {

                    ViewBag.ListImgProduct = (from p in _context.imageProduct where p.product.Name.IndexOf(s_name) >= 0 && p.Status == Convert.ToBoolean(s_stt) select p).ToList();
                }
            }
            return View(imageProduct);
        }

        // GET: Admin/ImageProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageProduct = await _context.imageProduct
                .Include(i => i.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageProduct == null)
            {
                return NotFound();
            }

            return View(imageProduct);
        }

        // GET: Admin/ImageProducts/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.product, "Id", "Name");
            return View();
        }

        // POST: Admin/ImageProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdProduct,Name,Status")] ImageProduct imageProduct,IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imageProduct);
                await _context.SaveChangesAsync();
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/images/products", imageProduct.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful.CopyToAsync(stream);
                }
                imageProduct.Name = imageProduct.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                _context.Update(imageProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduct"] = new SelectList(_context.product, "Id", "Name", imageProduct.IdProduct);
            return View(imageProduct);
        }

        // GET: Admin/ImageProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageProduct = await _context.imageProduct.FindAsync(id);
            if (imageProduct == null)
            {
                return NotFound();
            }
            ViewData["IdProduct"] = new SelectList(_context.product, "Id", "Name", imageProduct.IdProduct);
            return View(imageProduct);
        }

        // POST: Admin/ImageProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdProduct,Name,Status")] ImageProduct imageProduct,IFormFile ful)
        {
            if (id != imageProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ful != null)
                    {
                        string t = imageProduct.Id + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", imageProduct.Name);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", t);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ful.CopyToAsync(stream);
                        }
                        imageProduct.Name = t;
                        _context.Update(imageProduct);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        _context.Update(imageProduct);
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageProductExists(imageProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduct"] = new SelectList(_context.product, "Id", "Name", imageProduct.IdProduct);
            return View(imageProduct);
        }

    

        // GET: Admin/ImageProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageProduct = await _context.imageProduct
                .Include(i => i.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageProduct == null)
            {
                return NotFound();
            }

            return View(imageProduct);
        }

        // POST: Admin/ImageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageProduct = await _context.imageProduct.FindAsync(id);
            _context.imageProduct.Remove(imageProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageProductExists(int id)
        {
            return _context.imageProduct.Any(e => e.Id == id);
        }
    }
}
