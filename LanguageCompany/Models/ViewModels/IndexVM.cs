using LanguageCompany.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCompany.Models.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<Language> Languages { get; set; } = new List<Language>();
        public IEnumerable<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();
    }
}
