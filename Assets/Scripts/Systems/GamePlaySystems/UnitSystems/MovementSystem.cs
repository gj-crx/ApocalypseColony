using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;
using System.Threading.Tasks;
using Zenject;

namespace Systems
{
    public class MovementSystem : GameSystem, ISystem
    {
        public MovementSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
            OnUnitOperated += OperateUnitMovement;
        }

        public void OperateUnitMovement(Unit operatedUnit)
        {
            if (operatedUnit.AbleToMove != false && operatedUnit.ComponentMovement.HasLocalWay) 
                operatedUnit.Position = operatedUnit.ComponentMovement.IterateWayMovement(operatedUnit.Position, operatedUnit.MoveSpeed, timeIntervalInSeconds);
        }
    }
}
