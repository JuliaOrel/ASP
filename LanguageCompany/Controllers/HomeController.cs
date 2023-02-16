using LanguageCompany.Models;
using LanguageCompany.Models.DTO;
using LanguageCompany.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Language> _languages;
        private List<Company> _companies;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Company google = new Company {Id=1, Name="Google",Country="USA" };
            Company microsoft = new Company {Id=2, Name="Microsoft",Country="USA" };
            Company oracle = new Company {Id=3, Name="Oracle",Country="Czech" };
            _companies = new List<Company>
            {
                google,
                microsoft,
                oracle
            };

            Language language1 = new Language {Id=1, Name = "js", Company = google };
            Language language2 = new Language { Id = 2, Name = "TS", Company = google };
            Language language3 = new Language { Id = 3, Name = "C#", Company = microsoft };
            Language language4 = new Language { Id = 4, Name = "F#", Company = microsoft };
            Language language5 = new Language { Id = 5, Name = "VB", Company = microsoft };
            Language language6 = new Language { Id = 6, Name = "java", Company = oracle };
            _languages = new List<Language>
            {
                language1,
                language2,
                language3,
                language4,
                language5,
                language6

            };
        }

        public IActionResult Index(int companyId)
        {
            List<CompanyDTO> companies = _companies
                .Select(c=>new CompanyDTO {Id=c.Id, Name=c.Name})
                .ToList();
            companies.Insert(0, new CompanyDTO {Id=0,Name="All companies" });
            IndexVM indexVM = new IndexVM
            {
                Languages = _languages,
                Companies = companies,
            };
            if(companyId!=0)
            {
                indexVM.Languages = _languages
                    .Where(l => l.Company.Id == companyId);
            }
            return View(indexVM);
        }

        public IActionResult Privacy()
        {
            //return RedirectToAction(nameof(Index), new {companyId=2 });
            return RedirectToRoute("default", new {controller="Home", action="Index", companyId=2 });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
