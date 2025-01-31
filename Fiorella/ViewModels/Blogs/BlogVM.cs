using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Blogs
{
    public class BlogVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title can't be empty!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Title can't be empty!")]
        public string Description { get; set; }
    }
}
