using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    /// <summary>
    /// Can train new units
    /// </summary>
    [System.Serializable]
    public class UnitTrainingComponent : IUnitComponent
    {
        public float TrainingSpeed = 1.0f;
        public List<short> AllowedUnitIDsToTrain = new List<short>();

        public Queue<short> UnitTrainingQueue = new Queue<short>();
        public Vector3 UnitSpawningOffset { get { return unitSpawningOffset; } }
        [SerializeField]
        private Vector3 unitSpawningOffset = new Vector3(1, 0, 1);

        //
        [HideInInspector] public float CurrentTrainingTimer = 0;

        public void CopyStatsFrom(IUnitComponent from)
        {
            UnitTrainingComponent copyFrom = from as UnitTrainingComponent;
            TrainingSpeed = copyFrom.TrainingSpeed;
            AllowedUnitIDsToTrain = copyFrom.AllowedUnitIDsToTrain;

            unitSpawningOffset = copyFrom.unitSpawningOffset;
        }
       
    }
}
