using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eccomerce.Areas.Admin.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]        
        public string Image { get; set; }
        [Required]        
        public int Price { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int Catid { get; set; }
        [ForeignKey("Catid")]
        public virtual TypeProduct TypeP { get; set; }
    } 
}
