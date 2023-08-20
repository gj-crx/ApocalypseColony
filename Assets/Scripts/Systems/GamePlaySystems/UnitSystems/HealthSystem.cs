using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using System.Threading.Tasks;
using Zenject;

namespace Systems
{
    /// <summary>
    /// Applies regeneration for every unit
    /// </summary>
    public class HealthSystem : GameSystem, ISystem
    {

        public HealthSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
            OnUnitOperated += OperateUnitHealth;
        }

        public void OperateUnitHealth(Unit regeneratingUnit)
        {
            regeneratingUnit.CurrentHP += regeneratingUnit.RegenerationRate * timeIntervalInSeconds;
        }
    }
}
