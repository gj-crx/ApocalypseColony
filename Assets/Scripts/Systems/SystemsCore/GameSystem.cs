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
        public bool DebugLoggingActive = true;

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

        public async void SystemIterationCycle(int customTimeIntervalInMiliseconds = -1)
        {
            //-1 equals base value unchanged, so system supposed to use it's hardcoded value of a timeIntervalInSeconds, but it has to be converted to miliseconds first
            if (customTimeIntervalInMiliseconds != -1) timeIntervalInSeconds = customTimeIntervalInMiliseconds; //using custom interval
            else customTimeIntervalInMiliseconds = (int)(timeIntervalInSeconds * 1000); //using hardcoded interval but converted to miliseconds

            while (isActive)
            {
                try
                {
                    Unit[] unitsToOperate = dataBase.Units.ToArray();
                    for (short i = 0; i < unitsToOperate.Length; i++)
                    {
                        OnUnitOperated?.Invoke(GetNextUnitOfDataBaseToOperate(i, unitsToOperate));
                    }
                }
                catch(Exception exception)
                {
                    Debug.Log(dataBase == null);
                    Debug.Log("allah " + (dataBase.Units.ToArray() == null).ToString());
                    Debug.Log(GetNextUnitOfDataBaseToOperate(0, dataBase.Units.ToArray()));
                    Debug.Log(dataBase.Units.ToArray().Length);
                    Debug.LogError("System iteraction cycle failed " + OnUnitOperated.Method.Name + exception);  
                }
                await Task.Delay(customTimeIntervalInMiliseconds);
            }
        }
        protected void Log(string logInfoToShow, bool warning = false)
        {
            if (DebugLoggingActive == false) return;
            if (warning == false) Debug.Log(logInfoToShow);
            else Debug.LogWarning(logInfoToShow);
        }
        private Unit GetNextUnitOfDataBaseToOperate(short nextPossibleUnitIndex, Unit[] unitsToOperate)
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
