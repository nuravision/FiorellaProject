using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.ExpertVMS
{
    public class ExpertCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty!")]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This input can't be empty!")]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Speciality { get; set; }
        public string Image { get; set; }
    }
}
