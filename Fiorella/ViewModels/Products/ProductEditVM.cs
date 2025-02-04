using Fiorella.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Products
{
    public class ProductEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> NewImages { get; set; }
        public List<ProductEditImageVM> ExistImage {  get; set; }
    }
}


