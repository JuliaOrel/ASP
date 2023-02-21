using ASP_DZ_2_Model.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Models.ViewModels.IndexMoviesVM
{
    public class IndexMoviesVM
    {
        public IEnumerable<SessionDTO> Sessions { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public SelectList MoviesSL { get; set; }
        public int MovieId { get; set; }
        public string Search { get; set; }
    }
}
