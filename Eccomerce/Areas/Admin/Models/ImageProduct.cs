using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eccomerce.Areas.Admin.Models
{
    public class ImageProduct
    {
        [Key]
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product product { get; set; }
    }
}
