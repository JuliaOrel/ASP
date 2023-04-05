﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
