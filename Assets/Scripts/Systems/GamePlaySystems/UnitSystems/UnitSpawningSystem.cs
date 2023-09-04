using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;
using Factions;
using Zenject;
using System;

namespace Systems
{
    public class UnitSpawningSystem : GameSystem, ISystem
    {

        public UnitSpawningSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);

        public Unit SpawnNewUnit(short referenceUnitTypeID, Vector3 position, int factionID)
        {
            try
            {
                Unit newUnit = CreateNewUnitFromType(UnitTypesStorage.UnitTypes[referenceUnitTypeID]);
                dataBase.AddEntityToDataBase(newUnit);
                newUnit.UnitID = dataBase.GetIndexOfStoredEntity(newUnit);
                newUnit.Position = position;

                Faction unitFaction = dataBase.GetEntity(factionID, typeof(Faction)) as Faction;

                if (newUnit.Class == Unit.UnitClassification.RegularBuilding || newUnit.Class == Unit.UnitClassification.Townhall) unitFaction.Buildings.Add(newUnit);

                return newUnit;
            }
            catch(Exception error)
            {
                Debug.LogError("Unit spawning failed! " + error);
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
                TimeNeededToTrainThisUnit = unitType.TimeNeededToTrainThisUnit,
                ResourceCostToTrain = unitType.ResourceCostToTrain,

                //Collisions
                Body = BodyTypes.Body1X.GetBodyType(unitType.BodyType),
                CollisionRadius = unitType.CollisionRadius,
                IsExpellableByCollision = unitType.IsExpellableByCollision,

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
