using Fiorella.Data;
using Fiorella.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class FooterService : IFooterService
    {
        private readonly AppDbContext _context;
        public FooterService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.FooterNames.ToDictionaryAsync(m=>m.Key, m=>m.Value);
        }
    }
}
