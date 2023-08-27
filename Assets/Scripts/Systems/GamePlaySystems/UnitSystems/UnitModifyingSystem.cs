using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;
using Zenject;

namespace Systems
{
    public class UnitModifyingSystem : GameSystem, ISystem
    {

        public UnitModifyingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);

        public Unit SpawnNewUnit(short referenceUnitTypeID, Vector3 position)
        {
            try
            {
                Unit newUnit = CloneUnit(UnitTypesStorage.UnitTypes[referenceUnitTypeID]);
                dataBase.AddEntityToDataBase(newUnit);
                newUnit.UnitID = dataBase.GetIndexOfStoredEntity(newUnit);
                newUnit.Position = position;

                return newUnit;
            }
            catch
            {
                Debug.LogError("Unit spawning failed!");
                return null;
            }
        }
        public Unit CloneUnit(Unit unitToCloneFrom)
        {
            Unit clonedUnit = new Unit()
            {
                UnitTypeID = unitToCloneFrom.UnitTypeID,

                UnitName = unitToCloneFrom.UnitName,
                Class = unitToCloneFrom.Class,
                Position = unitToCloneFrom.Position,
                IsActive = true,
                MoveSpeed = unitToCloneFrom.MoveSpeed,
                Damage = unitToCloneFrom.Damage,
                AttackRange = unitToCloneFrom.AttackRange,
                AttackInterval = unitToCloneFrom.AttackInterval,
                MaxHP = unitToCloneFrom.MaxHP,
                RegenerationRate = unitToCloneFrom.RegenerationRate,
                Body = unitToCloneFrom.Body,

                AbleToAttack = unitToCloneFrom.AbleToAttack,                
                AbleToMove = unitToCloneFrom.AbleToMove,
                AbleToTrainUnits = unitToCloneFrom.AbleToTrainUnits
            };

            if (clonedUnit.AbleToAttack)
            {
                clonedUnit.ComponentAttacking = new AttackingComponent();
            }
            if (clonedUnit.AbleToMove)
            {
                clonedUnit.ComponentMovement = new UnitMovementComponent();
            }
            if (clonedUnit.AbleToTrainUnits)
            {
                clonedUnit.ComponentTraining = new UnitTrainingComponent();
                clonedUnit.ComponentTraining.CopyStatsFrom(unitToCloneFrom.ComponentTraining);
            }

            return clonedUnit;

        }
    }
}
