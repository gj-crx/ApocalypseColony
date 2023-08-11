using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;
using Zenject;

namespace Systems
{
    public abstract class GameSystem : ISystem
    {
        public bool IsActive { set { isActive = value; } }

        public delegate void UnitOperation(Unit operetaedUnit);
        public UnitOperation OnUnitOperated;

        [Inject] private GameDataBase dataBase;


        protected float timeIntervalInSeconds = 1.0f;
        private bool isActive = true;

        public string GetSystemName() => new string("Unnamed system");
        public async void SystemIterationCycle(int customTimeInterval = -1)
        {
            Debug.Log("system started");
            if (customTimeInterval != -1) timeIntervalInSeconds = customTimeInterval / 1000f;
            else customTimeInterval = (int)(timeIntervalInSeconds * 1000);

            while (isActive)
            {
                Unit[] unitsToOperate = dataBase.Units.ToArray();
                for (short i = 0; i < unitsToOperate.Length; i++)
                {
                    OnUnitOperated(GetNextUnit(i, unitsToOperate));
                }
                await Task.Delay(customTimeInterval);
            }
        }

        private Unit GetNextUnit(short nextPossibleUnitIndex, Unit[] unitsToOperate)
        {
            Unit nextUnit = null;
            try
            {
                if (unitsToOperate[nextPossibleUnitIndex] != null) nextUnit = unitsToOperate[nextPossibleUnitIndex];
            }
            catch { }

            return nextUnit;
        }
    }
}
