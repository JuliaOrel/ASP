using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_4_DI.Services
{
    public class Bazuka : IWeapon
    {
        public string Kill()
        {
            return "Kill with Bazuka";
        }
    }
}
