using Fiorella.Models;
using Fiorella.ViewModels.ExpertVMS;

namespace Fiorella.Services.Interfaces
{
    public interface IExpertsService
    {
        Task<List<Experts>> GetAllAsync();
        Task<List<Experts>> GetAllAsyncForAdmin();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(ExpertCreateVM expert);
    }
}
