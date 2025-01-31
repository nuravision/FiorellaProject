using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        public BlogsController(AppDbContext context,IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {   
            return View( await _blogService.GetAllOrderByDescendingAsync());
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog) {

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existBlog = await _blogService.ExistAsync(blog.Title);
            if (existBlog)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _blogService.CreateAsync(blog);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)  return BadRequest();
            Blog blog = await _blogService.GetWithProductsAsync((int)id);
            if (blog == null) return NotFound();
            BlogDetailVM blogModel = new()
            {
                Title = blog.Title,
                Description = blog.Description,
                Image = blog.Image,
                Date = blog.Date
            };
            return View(blogModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Delete(int? id)
        {
            if (id is null) return BadRequest();
            Blog blog = await _blogService.GetWithProductsAsync((int)id);
            if (blog == null) return NotFound();
            await _blogService.DeleteAsync(blog);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult>Edit(int? id)
        {
            if (id is null) return BadRequest();
            Blog blog=await _blogService.GetByIdAsync((int)id);
            if (blog == null) return NotFound();
            BlogEditVM editModel = new()
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description
            };
            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int? id, BlogEditVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();
            Blog existBlog = await _blogService.GetByIdAsync((int)id);
            if (existBlog is null) return NotFound();
           _blogService.EditAsync(existBlog, blog);
            return RedirectToAction(nameof(Index));
        }
    }
}
