using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eccomerce.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Eccomerce.Areas.Admin.Data
{
    public class DPContext : DbContext
    {

        public DPContext(DbContextOptions<DPContext> options)
            : base(options)
        {
        }
        public DbSet<Product> product { get; set; }
        public DbSet<TypeProduct> typeProduct { get; set; }
        public DbSet<Account> account { get; set; }
        public DbSet<TypeAccount> typeAccount { get; set; }
    }
}
