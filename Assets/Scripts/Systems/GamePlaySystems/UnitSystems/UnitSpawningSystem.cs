using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;
using Zenject;

namespace Systems
{
    public class UnitSpawningSystem : GameSystem, ISystem
    {

        public UnitSpawningSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);

        public Unit SpawnNewUnit(short referenceUnitTypeID, Vector3 position)
        {
            try
            {
                Unit newUnit = CreateNewUnitFromType(UnitTypesStorage.UnitTypes[referenceUnitTypeID]);
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
        private Unit CreateNewUnitFromType(UnitType unitType)
        {
            Unit clonedUnit = new Unit()
            {
                UnitTypeID = unitType.UnitTypeID,

                //general information
                UnitName = unitType.UnitName,
                Class = unitType.Class,
                Position = Vector3.zero,
                IsActive = true,
                //general stats
                MoveSpeed = unitType.MoveSpeed,
                Damage = unitType.Damage,
                AttackRange = unitType.AttackRange,
                AttackInterval = unitType.AttackInterval,
                MaxHP = unitType.MaxHP,
                RegenerationRate = unitType.RegenerationRate,
                //specialized stats
                Body = BodyTypes.Body1X.GetBodyType(unitType.BodyType),
                TimeNeededToTrainThisUnit = unitType.TimeNeededToTrainThisUnit,
                ResourceCostToTrain = unitType.ResourceCostToTrain,

                //possibilities 
                AbleToAttack = unitType.AbleToAttack,                
                AbleToMove = unitType.AbleToMove,
                AbleToTrainUnits = unitType.AbleToTrainUnits
            };

            if (clonedUnit.AbleToAttack)
            {
                clonedUnit.ComponentAttacking = new UnitAttackingComponent();
                clonedUnit.ComponentAttacking.CopyStatsFrom(unitType.ComponentAttacking);
            }
            if (clonedUnit.AbleToMove)
            {
                clonedUnit.ComponentMovement = new UnitMovementComponent();
                clonedUnit.ComponentMovement.CopyStatsFrom(unitType.ComponentMovement);
            }
            if (clonedUnit.AbleToTrainUnits)
            {
                clonedUnit.ComponentTraining = new UnitTrainingComponent();
                clonedUnit.ComponentTraining.CopyStatsFrom(unitType.ComponentTraining);
            }

            return clonedUnit;

        }
        /*     private Unit CloneUnitDeprecated(Unit unitToCloneFrom)
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

            }*/
    }
}
