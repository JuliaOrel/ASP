using Medicines.Data;
using Medicines.Extensions;
using Medicines.Data.Entities;
using Medicines.Models;
using Medicines.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _medicineContext;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _medicineContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Medicine()
        {
            IEnumerable<Medicine> medicines = _medicineContext.Medicines; 
          
            IndexMedicinesVM vM = new IndexMedicinesVM
            {
                Medicines =medicines
                
            };

            return View(vM);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _medicineContext.Medicines == null)
            {
                return NotFound();
            }

            Medicine medicine = await _medicineContext.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicine == null)
            {
                return NotFound();
            }
            HttpContext.Session.Set<Medicine>("LastViewedMedicines" + medicine.Id, medicine);
            DetailsMedicinesVM vM = new DetailsMedicinesVM
            {
                Medicine=medicine
            };


            return View(vM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
