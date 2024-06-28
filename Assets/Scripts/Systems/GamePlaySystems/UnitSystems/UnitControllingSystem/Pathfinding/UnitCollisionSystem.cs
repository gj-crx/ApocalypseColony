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
            OnUnitOperated += OperateCollisions;
        }

        private void OperateCollisions(Unit unit)
        {
            if (unit.IsExpellableByCollision == false) return;

            //Push neirby units away
            CollisionPush(dataBase.GetNearestUnitInShard(unit.Position, unit.CollisionRadius), unit.Position, 1);
        }
        private void CollisionPush(Unit unitToPush, Vector3 pushFrom, float pushingForce)
        {
            if (unitToPush == null) return;

            Vector3 pushingDirection = (pushFrom - unitToPush.Position).normalized;
            unitToPush.Position += pushingDirection * pushingForce;
        }
    }
}
