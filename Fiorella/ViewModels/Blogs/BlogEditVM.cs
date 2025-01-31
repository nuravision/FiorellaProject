using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Blogs
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This input can't be empty!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can't be empty!")]
        public string Description { get; set; }
    }
}
