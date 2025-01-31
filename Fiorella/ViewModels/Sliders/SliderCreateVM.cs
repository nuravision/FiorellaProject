using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
