using ASP_DZ_2_Model.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Models.ViewModels
{
    public class CreateMoviesVM
    {
        public SessionDTO Session { get; set; }
        public SelectList MoviesSL { get; set; }
    }
}
