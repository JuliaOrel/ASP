using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_4_DI.Services
{
    public class WarriorService
    {
        private readonly IWeapon _weapon;
        public WarriorService(IWeapon weapon)
        {
            _weapon = weapon;
        }
        public string KillMethod()
        {
            return _weapon.Kill();
        }
    }
}
