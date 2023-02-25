using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Data.Entities
{
    public class User: IdentityUser
    {
        public ICollection<Medicine> Medicines { get; set; }

    }
}
