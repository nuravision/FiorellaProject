using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels;
using Fiorella.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IExpertsService _expertsService;
        private readonly IHttpContextAccessor _accessor;
        public HomeController(AppDbContext context,IProductService productService,
            ICategoryService categoryService,IExpertsService expertsService,IHttpContextAccessor accessor)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _expertsService = expertsService;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {
           
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Product> products = await _productService.GetAllWithImagesAsync();
            List<Blog> blogs = await _context.Blogs.Take(3).ToListAsync();
            List<Experts> experts=await _expertsService.GetAllAsync();
            //string name = "Nunuuu";
            //_accessor.HttpContext.Response.Cookies.Append("name", name);
            HomeVM model = new()
            {
                
                Categories=categories,
                Products=products,
                Blogs=blogs,
                Experts = experts
                
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> AddProductToBasket(int? id)
        {
            if (id is null) return BadRequest();
            //var cookieData = _accessor.HttpContext.Request.Cookies["name"];
            List<BasketVM> basketProducts = null;
            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts=new List<BasketVM>();
            }
            var dbProduct=await _context.Products.FirstOrDefaultAsync(m=>m.Id==(int)id);
            var existProduct = basketProducts.FirstOrDefault(m => m.Id == (int)id);
            if (existProduct is not null) {
                existProduct.Count++;
            }
            else
            {
                basketProducts.Add(new BasketVM()
                {
                    Id = (int)id,
                    Count=1,
                    Price=dbProduct.Price
                });
            }
            
            _accessor.HttpContext.Response.Cookies.Append("basket",JsonConvert.SerializeObject(basketProducts));
            return RedirectToAction(nameof(Index));
        }
    }
}
