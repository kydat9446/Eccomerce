using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eccomerce.Areas.Admin.Models
{
    public class BillDetail
    {
        [Key]
        public int ID { get; set; }
        public int BillId { get; set; }
        [ForeignKey("BillId")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public int Amount { get; set; }
        public int Money { get; set; }
        public virtual Product Product { get; set; }
        public virtual Bill Bill { get; set; }
    }
}
