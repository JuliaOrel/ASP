﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_4_Beverage.Services
{
    public class Tea : IBeverage
    {
        public string Drink()
        {
            return "I drink tea";
        }
    }
}
