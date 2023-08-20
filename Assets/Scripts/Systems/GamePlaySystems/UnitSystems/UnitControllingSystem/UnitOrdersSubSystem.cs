using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Units;
using Systems.Pathfinding;

namespace Systems.UnitAI
{
    public static class UnitOrdersSubSystem
    {
        //go to target position attacking everyone on the way
        public static void SetAttackMoveOrder(Unit attackingUnit, Vector3 targetPosition, List<Unit> unitsToOperate, IPathfinding pathfindingSystem, bool onlyAttackRangeSearch = false)
        {
            if (attackingUnit.ComponentAttacking == null)
            {
                SetMoveOrder(attackingUnit, targetPosition, pathfindingSystem);
                return;
            }

            float searchDistance = attackingUnit.AttackRange * 4;
            if (attackingUnit.AbleToMove == false || onlyAttackRangeSearch) searchDistance = attackingUnit.AttackRange;

            float distanceToTarget;
            Unit target = UnitFinderSubSystem.FindClosestUnitInRange(attackingUnit, searchDistance, unitsToOperate, out distanceToTarget);

            if (target != null) SetUnitAttackOrder(attackingUnit, target, pathfindingSystem, distanceToTarget); //target is found, chasing it
            else SetMoveOrder(attackingUnit, targetPosition, pathfindingSystem); //no nearby targets, continue to move to position
        }

        //get to targeted unit and attack him ignoring other enemies
        public static void SetUnitAttackOrder(Unit attackingUnit, Unit target, IPathfinding pathfindingSystem, float distanceToTarget = -1)
        {
            if (attackingUnit.AbleToMove == false || pathfindingSystem == null)
            {
                attackingUnit.ComponentAttacking.CurrentTarget = target;
                return;
            }

            target.ComponentAttacking.CurrentTarget = target;
            if (distanceToTarget == -1) distanceToTarget = Vector3.Distance(attackingUnit.Position, target.Position);

            attackingUnit.CurrentOrder = new Unit.Order { Type = Unit.OrderType.AttackUnit, TargetPosition = target.Position, TargetUnit = target };

            if (distanceToTarget < attackingUnit.AttackRange) return; //no need to move anywhere

            attackingUnit.ComponentMovement.SetNewWay(pathfindingSystem.GetWayPath(attackingUnit.Position, target.Position, attackingUnit.Body), attackingUnit.Position); //get closely to the target
        }

        //just move to target position ignoring enemies
        public static void SetMoveOrder(Unit operatedUnit, Vector3 targetPosition, IPathfinding pathfindingSystem)
        {
            if (operatedUnit.AbleToMove == false || pathfindingSystem == null) return;
            if (operatedUnit.Position == targetPosition) return;

            operatedUnit.ComponentMovement.SetNewWay(pathfindingSystem.GetWayPath(operatedUnit.Position, targetPosition, operatedUnit.Body), operatedUnit.Position);

            if (operatedUnit.ComponentMovement.HasLocalWay) operatedUnit.CurrentOrder = new Unit.Order { Type = Unit.OrderType.MoveToPosition, TargetPosition = targetPosition, TargetUnit = null };
        }

        public static void AttackEnemiesInRange(Unit operatedUnit, List<Unit> unitsToOperate, IPathfinding pathfindingSystem, bool onlyAttackRangeSearch = false)
        {
            if (operatedUnit.ComponentAttacking == null) return;

            float searchDistance;
            if (onlyAttackRangeSearch || operatedUnit.AbleToMove == false) searchDistance = operatedUnit.AttackRange;
            else searchDistance = operatedUnit.AttackRange * 4;

            float distanceToTarget;
            Unit target = UnitFinderSubSystem.FindClosestUnitInRange(operatedUnit, searchDistance, unitsToOperate, out distanceToTarget);
            //target found, getting to it
            if (target != null)
            {
                if (distanceToTarget > operatedUnit.AttackRange) operatedUnit.ComponentMovement.SetNewWay(pathfindingSystem.GetWayPath(operatedUnit.Position, target.Position, operatedUnit.Body), operatedUnit.Position);
                operatedUnit.ComponentAttacking.CurrentTarget = target;
            }
        }
    }
}
