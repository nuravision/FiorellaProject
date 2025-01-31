using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels.Blogs;
using Fiorella.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class BlogService:IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<List<BlogVM>> GetAllOrderByDescendingAsync()
        {
            List<Blog> blogs = await _context.Blogs
                                            .OrderByDescending(m => m.Id)
                                             .ToListAsync();

            return blogs.Select(m => new BlogVM { Id = m.Id, Title = m.Title,Description=m.Description })
                 .ToList();
        }

        public async Task<bool> ExistAsync(string title)
        {
            return await _context.Blogs.AnyAsync(m=>m.Title.Trim() == title.Trim());
        }

        public async Task CreateAsync(BlogCreateVM blog)
        {
            await _context.Blogs.AddAsync(new Blog { Image = "h3-slider-background-2.jpg", Title = blog.Title, Description = blog.Description, Date = DateTime.Now });
            await _context.SaveChangesAsync();
        }

        public async Task<Blog> GetWithProductsAsync(int id)
        {
            return await _context.Blogs.Where(m => m.Id == id)
                                           .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task EditAsync(Blog blog, BlogEditVM blogEdit)
        {
            blog.Title = blogEdit.Title;
            blog.Description = blogEdit.Description;
            await _context.SaveChangesAsync();
        }
    }
}
