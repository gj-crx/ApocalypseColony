using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Systems.Pathfinding;
using Units;


namespace Systems
{
    public class UnitOrderProcessingSystem : GameSystem, ISystem
    {
        private IPathfinding pathfinding;
        private UnitAISystem unitAI;
        private UnitFightingSystem unitFighting;

        public UnitOrderProcessingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
            OnUnitOperated += OperateUnitOrders;
            pathfinding = systemsManager.GetSystem(typeof(NormalPathfinding)) as IPathfinding;
            unitAI = systemsManager.GetSystem(typeof(UnitAISystem)) as UnitAISystem;
        }

        public void OperateUnitOrders(Unit operatedUnit)
        {
            if (operatedUnit.CurrentOrder == null) return;

            Unit.Order currentOrder = operatedUnit.CurrentOrder;

            //check if target there is a target
            if (CheckIfAbleToAttackTargets(operatedUnit, currentOrder))
            { //checking if unit is able to attack target right away
                if (Vector3.Distance(operatedUnit.Position, currentOrder.TargetUnit.Position) < operatedUnit.AttackRange)
                { //Target in range
                    unitFighting.Hit(operatedUnit, operatedUnit.ComponentAttacking.CurrentTarget);
                    return;
                }
                else if (operatedUnit.AbleToMove)
                { //Target not in range, trying to get closer to it
                    var wayToTarget = pathfinding.GetWayPathFromUnitOrder(operatedUnit);
                    //checking if there is a way to get to the target
                    if (wayToTarget != null)
                    {
                        operatedUnit.ComponentMovement.SetNewWay(wayToTarget, operatedUnit.Position);
                        return;
                    }
                    else
                    { //target is unreachable, continuing moving orders if there are them

                    }
                }
            }
            //Move orders being executed if there are no priority targets
            OperateMovingOrders(operatedUnit, currentOrder);
        }
        private bool CheckIfAbleToAttackTargets(Unit operatedUnit, Unit.Order currentOrder)
        {
            if (operatedUnit.AbleToAttack == false) return false;

            if (currentOrder.Type == Unit.OrderType.AttackMove || currentOrder.Type == Unit.OrderType.OnAlarm)
            {
                var target = unitAI.GetNewTarget(operatedUnit);
                if (target != null) operatedUnit.ComponentAttacking.CurrentTarget = target;
                return target != null;
            }
            if (currentOrder.Type == Unit.OrderType.AttackUnit)
            {
                if (currentOrder.TargetUnit != null) return true;
                else operatedUnit.CurrentOrder = null;
            }

            return false;
        }
        private void OperateMovingOrders(Unit operatedUnit, Unit.Order currentOrder)
        {
            if (operatedUnit.AbleToMove == false) return;

            if (currentOrder.Type == Unit.OrderType.AttackMove || currentOrder.Type == Unit.OrderType.MoveToPosition)
            {
                operatedUnit.ComponentMovement.SetNewWay(pathfinding.GetWayPathFromUnitOrder(operatedUnit), operatedUnit.Position);
            }
        }
    }
}
