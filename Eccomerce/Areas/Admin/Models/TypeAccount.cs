﻿using System.ComponentModel.DataAnnotations;

namespace Eccomerce.Areas.Admin.Models
{
    public class TypeAccount
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tên loại")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}