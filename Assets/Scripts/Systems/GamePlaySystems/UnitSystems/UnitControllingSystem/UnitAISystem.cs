using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using Units;
using Systems.Pathfinding;
using System;
using Zenject;

using Systems.UnitAI;

namespace Systems
{
    /// <summary>
    /// Implements just an a basic commands that can be given to units
    /// </summary>
    public class UnitAISystem : GameSystem, ISystem
    {
        private IPathfinding pathfinding;
        private Queue<Tuple<Unit, Unit.Order>> queuedOrders = new Queue<Tuple<Unit, Unit.Order>>();
        
        public UnitAISystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
            OnUnitOperated += OperateUnitBehavior;
            pathfinding = systemsManager.GetSystem(typeof(NormalPathfinding)) as IPathfinding;
        }

        public void OperateUnitBehavior(Unit operatedUnit)
        {
            
        }
        public Unit GetNewTarget(Unit attackingUnit)
        {

        }
    }
}
