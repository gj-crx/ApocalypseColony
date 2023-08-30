using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitType", menuName = "ScriptableObjects/UnitType", order = 1)]
    public class UnitType : ScriptableObject
    {
        public string UnitName = "Unnamed unit";
        public Unit.UnitClassification Class;
        public short UnitTypeID = 0;

        //Public stat properties
        public float MoveSpeed = 3;
        public float Damage = 0;
        public float AttackRange = 2;
        public float AttackInterval = 1.5f;
        public float MaxHP = 100;
        public float RegenerationRate = 1;
        //Possibilities
        public bool AbleToMove = true;
        public bool AbleToAttack = true;
        public bool AbleToTrainUnits = true;
        //Specialized stats
        public float TimeNeededToTrainThisUnit = 10;
        public List<float> ResourceCostToTrain = new List<float>(5);

        public Pathfinding.BodyType BodyType;

        //Component for redacting stats
        public UnitMovementComponent ComponentMovement = null;
        public UnitAttackingComponent ComponentAttacking = null;
        public UnitTrainingComponent ComponentTraining = null;
    }
}