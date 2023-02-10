using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_4_Beverage.Services
{
    public class DrinkerService
    {
        private readonly IBeverage _beverage;
        public DrinkerService(IBeverage beverage)
        {
            _beverage = beverage;
        }
        public string DrinkMethod()
        {
            return _beverage.Drink();
        }
    }
}
