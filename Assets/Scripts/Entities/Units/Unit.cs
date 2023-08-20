using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Zenject;
using Pathfinding;

namespace Units
{
    [System.Serializable]
    public class Unit : ISynchronizableObject
    {
        //Public unit information
        public string UnitName = "Unnamed unit";
        public UnitClassification Class;
        public Vector3 Position = Vector3.zero;
        public bool IsActive = true;

        //NetInfo
    //    [HideInInspector] public Game CurrentGame { get; set; } = null;
        [HideInInspector] public int UnitID = 0;
        public short UnitTypeID = 0;

        //Optional unit components
        public UnitMovementComponent ComponentMovement = null;
        public AttackingComponent ComponentAttacking = null;
        public UnitTrainingComponent ComponentTraining = null;

        //Public stat properties
        public float MoveSpeed = 3;
        public float Damage = 0;
        public float AttackRange = 2;
        public float AttackInterval = 1.5f;
        public float MaxHP = 100;
        public float RegenerationRate = 1;

        public bool AbleToMove = true;
        public bool AbleToAttack = true;
        public bool AbleToTrainUnits = true;

        public float TrainingTime = 10;
        public List<float> ResourceCostToTrain = new List<float>(5);

        public IBodyType Body;

        //internal stat properties
        protected float currentHP = 100;
        protected Order currentOrder = null;
        [SerializeField] protected BodyType bodyType;

        public Unit()
        {
            Body = Body1X.GetBodyType(bodyType);
            Debug.Log("New unit " + UnitName);
        }

        public float CurrentHP
        {
            get { return currentHP; }
            set { currentHP = value < MaxHP ? value : MaxHP; }
        }
        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; }
        }

        public IEntityData GetDataToTransfer() => new UnitSeriazableData(UnitID, UnitTypeID, Position, CurrentHP);

        public void ApplyTransferedData(IEntityData transferedData) => ((UnitSeriazableData)transferedData).ApplyData(this);


        /// <summary>
        /// Contains all the information that has to be transfered to clients
        /// </summary>
        [System.Serializable]
        public struct UnitSeriazableData : IEntityData, INetworkSerializable
        {
            public int UnitID;
            public short UnitTypeID;
            public Vector3 Position;
            public float CurrentHP;

            public UnitSeriazableData(int UnitID, short UnitTypeID, Vector3 Position, float CurrentHP)
            {
                this.UnitID = UnitID;
                this.UnitTypeID = UnitTypeID;
                this.Position = Position;
                this.CurrentHP = CurrentHP;
            }
            public void ApplyData(Unit unitToTransferDataInto)
            {
                unitToTransferDataInto.Position = Position;
                unitToTransferDataInto.CurrentHP = CurrentHP;
            }
            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref UnitID);
                serializer.SerializeValue(ref UnitTypeID);
                serializer.SerializeValue(ref Position);
                serializer.SerializeValue(ref CurrentHP);
            }
        }
        public class Order
        {
            public OrderType Type = OrderType.Hold;

            public Vector3 TargetPosition = Vector3.zero;
            public Unit TargetUnit = null;
        }
        public enum OrderType : byte
        {
            OnAlarm = 0,
            Hold = 1,
            AttackMove = 2,
            AttackUnit = 3,
            MoveToPosition = 4
        }
        public enum UnitClassification : byte
        {
            //movable units
            RegularUnit = 0,
            Worker = 1,
            BasicFighter = 2,
            RangedFighter = 3,

            //buildings
            RegularBuilding = 100,
            Townhall = 101
        }
    }
}
