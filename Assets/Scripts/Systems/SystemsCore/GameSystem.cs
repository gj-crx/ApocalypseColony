using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;
using Zenject;
using System;

namespace Systems
{
    public abstract class GameSystem
    {
        public bool IsActive { set { isActive = value; } }

        public delegate void UnitOperation(Unit operetaedUnit);
        public UnitOperation OnUnitOperated;

        internal GameDataBase dataBase;


        protected float timeIntervalInSeconds = 1.0f;
        private bool isActive = true;

        public GameSystem(GameSystemsManager systemsManager)
        {
            Resolve(systemsManager);
        }
        protected virtual void Resolve(GameSystemsManager systemsManager)
        {
            dataBase = systemsManager.GetDataBase();
        }

        public async void SystemIterationCycle(int customTimeInterval = -1)
        {
            if (customTimeInterval != -1) timeIntervalInSeconds = customTimeInterval / 1000f;
            else customTimeInterval = (int)(timeIntervalInSeconds * 1000);

            while (isActive)
            {
                try
                {
                    Unit[] unitsToOperate = dataBase.Units.ToArray();
                    for (short i = 0; i < unitsToOperate.Length; i++)
                    {
                        OnUnitOperated?.Invoke(GetNextUnit(i, unitsToOperate));
                    }
                }
                catch(Exception exception)
                {
                    Debug.Log(dataBase == null);
                    Debug.Log("allah " + (dataBase.Units.ToArray() == null).ToString());
                    Debug.Log(GetNextUnit(0, dataBase.Units.ToArray()));
                    Debug.Log(dataBase.Units.ToArray().Length);
                    Debug.LogError("System iteraction cycle failed " + OnUnitOperated.Method.Name + exception);  
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
