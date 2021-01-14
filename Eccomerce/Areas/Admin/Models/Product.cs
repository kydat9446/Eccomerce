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
        [Display(Name="Tên sản phẩm")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Giá")]
        public int Price { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Loại sản phẩm")]
        public int Catid { get; set; }
        [ForeignKey("Catid")]
        [Display(Name = "Loại sản phẩm")]
        public virtual TypeProduct TypeP { get; set; }
    } 
}
