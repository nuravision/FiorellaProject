using Fiorella.Models;
using Fiorella.ViewModels.Blogs;
using Fiorella.ViewModels.Categories;

namespace Fiorella.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(BlogCreateVM blog);
        Task<Blog> GetWithProductsAsync(int id);
        Task DeleteAsync(Blog blog);
        Task<Blog> GetByIdAsync(int id);
        Task EditAsync(Blog blog, BlogEditVM blogEdit);




    }
}
