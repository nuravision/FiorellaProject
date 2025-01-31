﻿using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty!")]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }

    }
}
