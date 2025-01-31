namespace Fiorella.Services.Interfaces
{
    public interface IFooterService
    {
        Task<Dictionary<string, string>> GetAllAsync();
    }
}
