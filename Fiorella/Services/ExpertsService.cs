using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels.ExpertVMS;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class ExpertsService : IExpertsService
    {
        private readonly AppDbContext _context;
        public ExpertsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Experts.AnyAsync(m=>m.Name.Trim() == name.Trim());
        }

        public async Task<List<Experts>> GetAllAsync()
        {
            return await _context.Experts.Take(4).ToListAsync();
        }

        public async Task<List<Experts>> GetAllAsyncForAdmin()
        {
           return await _context.Experts.OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task CreateAsync(ExpertCreateVM expert)
        {
            await _context.Experts.AddAsync(new Experts { Name = expert.Name,Speciality = expert.Speciality ,Image= "testimonial-img-1.png" });
            await _context.SaveChangesAsync();
        }
    }
}
