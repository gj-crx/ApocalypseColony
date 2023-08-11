using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;
using System.Threading.Tasks;

namespace Systems
{
    public class MovementSystem : GameSystem, ISystem
    {

        private int testnigger = 1;
        public MovementSystem()
        {
            OnUnitOperated += OperateUnitMovement;
            testnigger = Random.Range(-100, 100);
        }

        public void OperateUnitMovement(Unit operatedUnit)
        {
            if (operatedUnit.AbleToMove != false && operatedUnit.ComponentMovement.HasLocalWay) 
                operatedUnit.Position = operatedUnit.ComponentMovement.IterateWayMovement(operatedUnit.Position, operatedUnit.MoveSpeed, timeIntervalInSeconds);
        }
    }
}
