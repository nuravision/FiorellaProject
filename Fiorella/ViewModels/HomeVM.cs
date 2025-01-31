using Fiorella.Models;

namespace Fiorella.ViewModels
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Experts> Experts { get; set; }

    }
}
