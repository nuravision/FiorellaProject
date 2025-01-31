using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class Experts:BaseEntity
    {
        [Required(ErrorMessage = "This input can't be empty!")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "This input can't be empty!")]
        public string Speciality { get; set; }
    }
}
