using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatsCRUD.Data.Entities
{
    public class Breed
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Breed Name")]
        public string BreedName { get; set; }
        public ICollection<Cat> Cats { get; set; }
    }
}