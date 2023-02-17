using CatsCRUD.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Models.DTO
{
    public class CatDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cat Name")]
        public string CatName { get; set; }
        public string Description { get; set; }
        [Required]
        public CatGender Gender { get; set; }
        public bool Vaccinated { get; set; }
        public byte[] Image { get; set; }
        
        public int BreedId { get; set; }
        public BreedDTO Breed { get; set; }
    }
}
