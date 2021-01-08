using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eccomerce.Areas.Admin.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public int Total { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

        public virtual Account Customer { get; set; }
    }
}
