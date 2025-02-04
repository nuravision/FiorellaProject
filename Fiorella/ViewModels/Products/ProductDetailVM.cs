namespace Fiorella.ViewModels.Products
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<ProductImageVM> Images { get; set; }
    }
}
