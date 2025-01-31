using Fiorella.Data;
using Fiorella.Models;
using Fiorella.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task< IActionResult > Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<SliderVM>result=sliders.Select(m=>new SliderVM { Id=m.Id,Image=m.Image}).ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View();
            if (!request.Image.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "File must be only image format");
                return View();
            }
            if (request.Image.Length/1024>200)
            {
                ModelState.AddModelError("Image", "Image size must be max 200 kb.");
                return View();
            }
            return View();
        }

    }
}
