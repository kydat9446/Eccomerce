using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eccomerce.Areas.Admin.Data;
using Eccomerce.Areas.Admin.Models;
namespace Eccomerce.ViewComponents
{
    public class CategoriesNavViewComponent: ViewComponent
    {
        private readonly DPContext _context;

        public CategoriesNavViewComponent(DPContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool Status)
        {
            var items = await GetItemsAsync(Status);
            return View(items);
        }
        private Task<List<TypeProduct>> GetItemsAsync(bool Status)
        {
            return _context.typeProduct.Where(x => x.Status == Status).ToListAsync();
        }
    }
}
