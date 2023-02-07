using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductStore2.Data;
using ProductStore2.Models;
using ProductStore2.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductservice _productService; 
        

        public HomeController(ILogger<HomeController> logger, IProductservice productservice)
        {
            _logger = logger;
            _productService = productservice;
    
        }

        public async Task <ActionResult<IEnumerable<Product>>> IndexAsync()
        {
            IQueryable<Product> products = _productService.GetProducts();
            
            return View(await products.ToListAsync());
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            ViewBag.ProductId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy([Bind(nameof(Order.UserName), nameof(Order.ContactPhone),
            nameof(Order.ProductId))] Order order)
        {
            if (ModelState.IsValid == false)
            {
                return View(order);
            }
            await _productService.Buy(order);
            //return Content($"Thanks, {order.UserName} for purchase");
            return View("BuyedProduct", order.UserName);
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
