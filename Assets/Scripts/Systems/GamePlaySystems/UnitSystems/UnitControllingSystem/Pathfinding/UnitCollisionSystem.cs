using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;

namespace Systems
{
    public class UnitCollisionSystem : GameSystem, ISystem
    {


        public UnitCollisionSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
        }

        private void OperateCollisions(Unit unit)
        {
            if (unit.IsExpellableByCollision == false) return;

            //Push neirby units away
            dataBase.GetNearestUnitInShard.
        }
    }
}
