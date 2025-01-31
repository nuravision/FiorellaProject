﻿using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class Category:BaseEntity
    {

        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
