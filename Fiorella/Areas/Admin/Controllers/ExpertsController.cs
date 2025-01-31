using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels.Blogs;
using Fiorella.ViewModels.ExpertVMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExpertsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IExpertsService _expertsService;
        public ExpertsController(AppDbContext context, IExpertsService expertsService)
        {
            _context = context;
            _expertsService = expertsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _expertsService.GetAllAsyncForAdmin());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertCreateVM expert)
        {

            if (ModelState.IsValid) { return View(); }
            bool existExpert = await _expertsService.ExistAsync(expert.Name);
            if (existExpert)
            {
                ModelState.AddModelError("Name", "This expert name already exist");
                return View();
            }
            await _expertsService.CreateAsync(expert);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Experts expert = await _context.Experts.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (expert is null) return NotFound();
            return View(expert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Experts expert = await _context.Experts.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (expert is null) return NotFound();
            _context.Experts.Remove(expert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Experts experts = await _context.Experts.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (experts is null) return NotFound();
            Experts editExpert = new()
            {
                Name = experts.Name,
                Speciality = experts.Speciality,
                Image = experts.Image,
            };
            return View(editExpert);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Experts expert)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();
            Experts existExperts=await _context.Experts.Where(m=>m.Id == id).FirstOrDefaultAsync();
            if(existExperts is null) return NotFound();
            existExperts.Name = expert.Name;
            existExperts.Speciality = expert.Speciality;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
