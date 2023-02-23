using ASP_DZ_2_Model.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Models.ViewModels
{
    public class EditMoviesVM
    {
        public MovieDTO Movie { get; set; }
        [Required]
        public List<Session> Sessions { get; set; }
        public int CountOfSessions
        {
            get
            {
                return Sessions is null ? 0 : Sessions.Count;
            }
        }
    }
}
