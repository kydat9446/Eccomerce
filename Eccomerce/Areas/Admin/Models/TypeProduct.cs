﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Eccomerce.Areas.Admin.Models
{
    public class TypeProduct
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}