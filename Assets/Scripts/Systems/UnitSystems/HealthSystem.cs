using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using System.Threading.Tasks;

namespace Systems
{
    /// <summary>
    /// Applies regeneration for every unit
    /// </summary>
    public class HealthSystem : GameSystem
    {
        public HealthSystem()
        {
            OnUnitOperated += OperateUnitHealth;
        }

        public void OperateUnitHealth(Unit regeneratingUnit)
        {
            regeneratingUnit.CurrentHP += regeneratingUnit.RegenerationRate * timeIntervalInSeconds;
        }
    }
}
