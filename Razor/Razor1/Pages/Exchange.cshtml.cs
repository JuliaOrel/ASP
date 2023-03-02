using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor1.Pages
{
    public class ExchangeModel : PageModel
    {
        public string SomeMessage { get; set; }
        private readonly decimal currency = 36.67M;
        public void OnGet()
        {
            SomeMessage = "Enter the sum";
        }

        public void OnPost(int? sum)
        {
            if(sum == null)
            {
                SomeMessage = "Invalid sum";
            }
            else
            {
                decimal result = sum.Value / currency;
                SomeMessage = $"{sum} UAH = {result.ToString("F2")}";
            }
        }

    }
}
