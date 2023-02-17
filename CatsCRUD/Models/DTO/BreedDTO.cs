using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatsCRUD.Models.DTO
{
    public class BreedDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Breed Name")]
        public string BreedName { get; set; }
        public ICollection<CatDTO> Cats { get; set; }
    }
}