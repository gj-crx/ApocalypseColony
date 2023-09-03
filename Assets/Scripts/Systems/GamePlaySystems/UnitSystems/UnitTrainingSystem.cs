using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;

namespace Systems
{
    public class UnitTrainingSystem : GameSystem, ISystem
    {
        private UnitSpawningSystem unitModifying;

        float timeStep = 1;


        public UnitTrainingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
            OnUnitOperated += TrainingQueueIteration;
            unitModifying = (UnitSpawningSystem)systemsManager.GetSystem(typeof(UnitSpawningSystem));
        }

        public void TrainingQueueIteration(Unit trainingUnit)
        {
            Log("training q iteration");
            if (trainingUnit.AbleToTrainUnits && trainingUnit.ComponentTraining.UnitTrainingQueue.Count > 0)
            {
                if (trainingUnit.ComponentTraining.CurrentTrainingTimer > UnitTypesStorage.UnitTypes[trainingUnit.ComponentTraining.UnitTrainingQueue.Peek()].TimeNeededToTrainThisUnit)
                { //Enough time passed to train current unit
                    unitModifying.SpawnNewUnit(trainingUnit.ComponentTraining.UnitTrainingQueue.Dequeue(),
                        trainingUnit.Position + trainingUnit.ComponentTraining.UnitSpawningOffset, trainingUnit.FactionID);

                    trainingUnit.ComponentTraining.CurrentTrainingTimer = 0;
                }
                else trainingUnit.ComponentTraining.CurrentTrainingTimer += timeStep * trainingUnit.ComponentTraining.TrainingSpeed;
            }
        }

        public bool AddUnitToTrainingQueue(Unit trainingUnit , short unitTypeIDToTrain, float[] resourceStorage)
        {
            if (trainingUnit.AbleToTrainUnits == false) { Debug.LogError("This unit is unable to train!"); return false; }

            //check if this building can train this type of a unit
            if (trainingUnit.ComponentTraining.AllowedUnitIDsToTrain.Contains(unitTypeIDToTrain) == false) return false;

            UnitType unitToTrainType = UnitTypesStorage.UnitTypes[unitTypeIDToTrain];
            CheckResourceStoragesCompatability(resourceStorage, unitToTrainType.ResourceCostToTrain);

            //check if it is enough resources
            bool enoughResourcesToTrain = true;
            for (int resourceIndex = 0; resourceIndex < resourceStorage.Length; resourceIndex++)
            {
                if (unitToTrainType.ResourceCostToTrain[resourceIndex] > resourceStorage[resourceIndex])
                {
                    enoughResourcesToTrain = false;
                    break;
                }
            }
            if (enoughResourcesToTrain == false) return false;

            //actually drain resources and add to the queue
            for (int resourceIndex = 0; resourceIndex < unitToTrainType.ResourceCostToTrain.Count; resourceIndex++) resourceStorage[resourceIndex] -= unitToTrainType.ResourceCostToTrain[resourceIndex];

            trainingUnit.ComponentTraining.UnitTrainingQueue.Enqueue(unitTypeIDToTrain);
            return true;
        }
        private void CheckResourceStoragesCompatability(float[] storage1, List<float> storage2)
        {
            if (storage1.Length != storage2.Count)
            {
                Debug.LogError("Resource storages compatability error");
                storage2.Capacity = storage1.Length;
            }
        }
    }
}
