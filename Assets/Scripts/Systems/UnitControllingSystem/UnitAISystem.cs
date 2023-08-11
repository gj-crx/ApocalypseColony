using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using Units;
using Pathfinding;
using Systems.UnitAI;
using System;
using Zenject;

namespace Systems
{
    /// <summary>
    /// Implements just an a basic commands that can be given to units
    /// </summary>
    public class UnitAISystem : GameSystem, ISystem
    {
        [Inject]
        private IPathfinding pathfindingSubSystem;
        private List<Unit> unitsToOperate;
        private Queue<Tuple<Unit, Unit.Order>> queuedOrders = new Queue<Tuple<Unit, Unit.Order>>();
        
        public UnitAISystem()
        {
            OnUnitOperated += OperateOrderQueue;
        }

        public void OperateOrderQueue(Unit notOperatedUnit)
        {
            IssueQueuedOrders();
            OperateOngoingUnitOrders(timeIntervalInSeconds);
        }

        public void EnqueueNewOrder(Unit unit, Unit.Order order) => queuedOrders.Enqueue(new Tuple<Unit, Unit.Order>(unit, order));

        /// <summary>
        /// Issues new orders to units when needed only
        /// </summary>
        private void IssueQueuedOrders()
        {
            while (queuedOrders.Count > 0)
            {
                var currentOrder = queuedOrders.Dequeue();
                Debug.Log("Order dequeued" + currentOrder.Item2.Type);

                if (currentOrder.Item2.Type == Unit.OrderType.AttackMove) UnitOrdersSubSystem.SetAttackMoveOrder(currentOrder.Item1, currentOrder.Item2.TargetPosition, unitsToOperate, pathfindingSubSystem);
                if (currentOrder.Item2.Type == Unit.OrderType.AttackUnit) UnitOrdersSubSystem.SetUnitAttackOrder(currentOrder.Item1, currentOrder.Item2.TargetUnit, pathfindingSubSystem);
                if (currentOrder.Item2.Type == Unit.OrderType.MoveToPosition) UnitOrdersSubSystem.SetMoveOrder(currentOrder.Item1, currentOrder.Item2.TargetPosition, pathfindingSubSystem);

                currentOrder.Item1.CurrentOrder = currentOrder.Item2;
                Debug.Log("ordering finished");
            }
        }
        /// <summary>
        /// Just supports ongoing unit orders, does not implements any new orders
        /// </summary>
        private void OperateOngoingUnitOrders(float timeIntervalInSeconds)
        {
            foreach (var operatedUnit in unitsToOperate)
            {
                if (operatedUnit.CurrentOrder != null)
                {
                    if (operatedUnit.CurrentOrder.Type == Unit.OrderType.OnAlarm) UnitOrdersSubSystem.AttackEnemiesInRange(operatedUnit, unitsToOperate, pathfindingSubSystem);
                    if (operatedUnit.CurrentOrder.Type == Unit.OrderType.Hold) UnitOrdersSubSystem.AttackEnemiesInRange(operatedUnit, unitsToOperate, pathfindingSubSystem, true);
                    if (operatedUnit.CurrentOrder.Type == Unit.OrderType.AttackMove) UnitOrdersSubSystem.AttackEnemiesInRange(operatedUnit, unitsToOperate, pathfindingSubSystem);
                }
            }
        }
    }
}
