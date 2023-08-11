using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;

namespace Systems
{
    public class BuildingOperationSystem : GameSystem, ISystem
    {

        public BuildingOperationSystem()
        {
            OnUnitOperated += OperateTrainingQueue;
        }
        public void OperateTrainingQueue(Unit trainingBuilding)
        {
            if (trainingBuilding != null && trainingBuilding.AbleToTrainUnits)
                trainingBuilding.ComponentTraining.TrainingQueueIteration(timeIntervalInSeconds, trainingBuilding.CurrentGame, trainingBuilding.Position);
        }
    }
}
