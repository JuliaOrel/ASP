﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
    public class CompanyDetailsDTO: CompanyDTO
    {
        public IEnumerable<CarDTO> Cars { get; set; }
    }
}
