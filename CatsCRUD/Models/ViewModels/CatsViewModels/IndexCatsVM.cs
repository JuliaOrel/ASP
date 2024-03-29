﻿using CatsCRUD.Data.Entities;
using CatsCRUD.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Models.ViewModels.CatsViewModels
{
    public class IndexCatsVM
    {
        public IEnumerable<CatDTO> Cats { get; set; }
        public SelectList BreedSL { get; set; }
        public int BreedId { get; set; }
        public string Search { get; set; }
    }
}
