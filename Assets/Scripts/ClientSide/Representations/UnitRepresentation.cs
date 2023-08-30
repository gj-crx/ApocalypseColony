using ClientSideLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.ClientSide {
    /// <summary>
    /// Does not contains any actual logic, just client side representation
    /// </summary>
    public class UnitRepresentation : MonoBehaviour, ISynchronizableObject
    {
        public int UnitID = 0;

        //seriazable data
        public Vector3 LastUpdatedPosition;
        public float CurrentHP;


        //Being used just to interpolate 
        private UnitMovementComponent clientSideMovement = new UnitMovementComponent();

        [SerializeField] private int minimalInterpolationSpeed = 2;
        [SerializeField] private float maxInterpolationSpeedModifier = 0.9f;


        public static void CreateUnitRepresentationOnAClient(Unit.UnitSeriazableData unitToCreateData)
        {
            UnitRepresentation newUnitRepresentation = 
                GameObject.Instantiate(PrefabManager.UnitRepresentationPrefabs[unitToCreateData.UnitTypeID], unitToCreateData.Position, Quaternion.identity).GetComponent<UnitRepresentation>();

            newUnitRepresentation.UnitID = unitToCreateData.UnitID;
            newUnitRepresentation.LastUpdatedPosition = unitToCreateData.Position;

            newUnitRepresentation.gameObject.name = UnitTypesStorage.UnitTypes[unitToCreateData.UnitTypeID].UnitName + unitToCreateData.UnitID.ToString();

            ClientDataBase.Singleton.AddEntityToDataBase(newUnitRepresentation);
            Debug.Log("Unit recieved " + unitToCreateData.UnitID);
        }
        public void ApplyData(Unit.UnitSeriazableData dataToApply)
        {
            LastUpdatedPosition = dataToApply.Position;
            CurrentHP = dataToApply.CurrentHP;
        }

        public void ApplyTransferedData(IEntityData transferedData)
        {
            Unit.UnitSeriazableData data = (Unit.UnitSeriazableData)transferedData;

            LastUpdatedPosition = data.Position;
            CurrentHP = data.CurrentHP;
        }

        public void DeathClientSide()
        {

        }

        public IEntityData GetDataToTransfer()
        {
            throw new System.NotImplementedException();
        }

        private void FixedUpdate() => PositionInterpolation();
        private void PositionInterpolation()
        {
            if (LastUpdatedPosition != transform.position)
            {
                float speed = Mathf.Max(minimalInterpolationSpeed, (LastUpdatedPosition - transform.position).magnitude * maxInterpolationSpeedModifier);
                clientSideMovement.SetNewWay(new Vector3[] { transform.position, LastUpdatedPosition }, transform.position);
                transform.position = clientSideMovement.IterateWayMovement(transform.position, speed, Time.fixedDeltaTime);
            }
        }
    }
}
