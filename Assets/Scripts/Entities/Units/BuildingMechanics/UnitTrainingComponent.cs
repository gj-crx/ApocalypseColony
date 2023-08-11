using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    /// <summary>
    /// Can train new units
    /// </summary>
    [System.Serializable]
    public class UnitTrainingComponent
    {
        public float TrainingSpeed = 1.0f;
        public List<short> AllowedUnitIDsToTrain = new List<short>();

        public Queue<short> UnitTrainingQueue = new Queue<short>();
        public float TrainingTimer = 0;
        public Vector3 UnitSpawningPositionOffset { get { return unitSpawningPositionOffset; } }


        [SerializeField]
        private Vector3 unitSpawningPositionOffset = new Vector3(1, 0, 1);
        
        public void CopyStatsFrom(UnitTrainingComponent from)
        {
            TrainingSpeed = from.TrainingSpeed;
            AllowedUnitIDsToTrain = from.AllowedUnitIDsToTrain;

            unitSpawningPositionOffset = from.unitSpawningPositionOffset;
        }
       
    }
}
