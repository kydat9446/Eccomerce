using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eccomerce.Areas.Admin.Models
{
    public class Account
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        public int Catid { get; set; }
        [ForeignKey("Catid")]
        [Display(Name = "Loại tài khoản")]
        public virtual TypeAccount TypeA { get; set; }
    }
}
