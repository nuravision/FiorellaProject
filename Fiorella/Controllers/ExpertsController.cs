using Fiorella.Data;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class ExpertsController : Controller
    {
        private readonly AppDbContext _context;
        public ExpertsController(AppDbContext context)
        {
            _context = context;
        }
        public  async Task< IActionResult> Index()
        {
            List<Experts>experts=await _context.Experts.ToListAsync();
            return View(experts);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            Experts expert=await _context.Experts.Where(m=>m.Id==id).FirstOrDefaultAsync();
            return View(expert);
        }
    }
}
