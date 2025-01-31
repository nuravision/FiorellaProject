using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IExpertsService _expertsService;
        public HomeController(AppDbContext context,IProductService productService,ICategoryService categoryService,IExpertsService expertsService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _expertsService = expertsService;
        }
        public async Task<IActionResult> Index()
        {
           
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Product> products = await _productService.GetAllAsync();
            List<Blog> blogs = await _context.Blogs.Take(3).ToListAsync();
            List<Experts> experts=await _expertsService.GetAllAsync();
            HomeVM model = new()
            {
                
                Categories=categories,
                Products=products,
                Blogs=blogs,
                Experts = experts
                
            };
            return View(model);
        }
    }
}
