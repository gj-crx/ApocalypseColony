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
        [Inject] private IDataBase dataBase;

        public Unit SpawnNewUnit(short referenceUnitTypeID, Vector3 position, Game associatedGame)
        {
            Unit newUnit = CloneUnit(UnitTypesStorage.UnitTypes[referenceUnitTypeID], associatedGame);
            dataBase.AddEntityToDataBase(newUnit);
            newUnit.UnitID = dataBase.GetIndexOfStoredEntity(newUnit);
            newUnit.Position = position;

            return newUnit;
        }
        public Unit CloneUnit(Unit unitToCloneFrom, Game clonedUnitGame)
        {
            Unit clonedUnit = new Unit()
            {
                UnitTypeID = unitToCloneFrom.UnitTypeID,
                CurrentGame = clonedUnitGame,

                UnitName = unitToCloneFrom.UnitName,
                Class = unitToCloneFrom.Class,
                Position = unitToCloneFrom.Position,
                IsActive = clonedUnitGame != null,
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
