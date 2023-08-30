using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [System.Serializable]
    public class UnitMovementComponent : IUnitComponent
    {
        public bool HasLocalWay { get { return localWay != null; } }

        [SerializeField] private Vector3[] localWay = null;
        [SerializeField] private Vector3 currentDirection = Vector3.zero;
        [SerializeField] private int currentDistance = 1;




        public void SetNewWay(Vector3[] newWay, Vector3 currentUnitPosition)
        {
            localWay = newWay;
            currentDistance = 1;
            currentDirection = GetDirection(currentUnitPosition, true);
        }

        private Vector3 GetDirection(Vector3 currentPosition, bool useZCordInsteadOfY)
        {
            Vector3 result = (localWay[currentDistance] - currentPosition).normalized;
            if (useZCordInsteadOfY) return new Vector3(result.x, 0, result.z);
            else return new Vector3(result.x, result.y, 0);

        }

        private bool PositionEqualsTo(Vector3 positionToCheck1, Vector3 positionToCheck2, bool useZCordInsteadOfY)
        {
            if (useZCordInsteadOfY) return (positionToCheck1.x == positionToCheck2.x && positionToCheck1.z == positionToCheck2.z);
            else return (positionToCheck1.x == positionToCheck2.x && positionToCheck1.y == positionToCheck2.y);
        }
        /// <summary>
        /// Returns new position
        /// </summary>
        public Vector3 IterateWayMovement(Vector3 currentPosition, float speed, float timeStep)
        {
            //Moving to a new position
            Vector3 newPosition = currentPosition + currentDirection * speed * timeStep;

            //Way point correction in case target is partially reached
            if (currentDirection.x > 0 && newPosition.x > localWay[currentDistance].x) newPosition = new Vector3(localWay[currentDistance].x, newPosition.y, newPosition.z);
            if (currentDirection.x < 0 && newPosition.x < localWay[currentDistance].x) newPosition = new Vector3(localWay[currentDistance].x, newPosition.y, newPosition.z);
            if (currentDirection.z > 0 && newPosition.z > localWay[currentDistance].z) newPosition = new Vector3(newPosition.x, newPosition.y, localWay[currentDistance].z);
            if (currentDirection.z < 0 && newPosition.z < localWay[currentDistance].z) newPosition = new Vector3(newPosition.x, newPosition.y, localWay[currentDistance].z);

            //Target is reached check
            if (PositionEqualsTo(newPosition, localWay[currentDistance], true))
            {
                currentDistance++;
                if (currentDistance >= localWay.Length)
                { //target is reached
                    localWay = null;
                }
                else currentDirection = GetDirection(currentPosition, true);
            }

         //   Debug.Log("Waymoving old position: " + currentPosition + " new position: " + newPosition);

            return newPosition;
        }
        public void CopyStatsFrom(IUnitComponent from)
        {
            var copyFrom = from as UnitMovementComponent;

        }

    }
}
