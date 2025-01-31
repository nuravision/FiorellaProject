using Fiorella.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly IFooterService _footerService;
        public FooterViewComponent(IFooterService footerService)
        {
            _footerService = footerService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _footerService.GetAllAsync()));
        }
    }
}
