using Medicines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Models.ViewModels
{
    public class IndexMedicinesVM
    {
        public IEnumerable<Medicine> Medicines { get; set; }
    }
}
