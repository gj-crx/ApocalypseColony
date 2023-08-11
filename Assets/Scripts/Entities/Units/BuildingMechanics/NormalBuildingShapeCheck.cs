using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Buildings
{
    public class NormalBuildingShapeCheck : IShapeCheck
    {
        [SerializeField]
        private Transform boxCheckAreaNoGround;
        [SerializeField]
        private int groundMaskID = 3;

        public bool IsValidPositionForBuilding
        {
            get
            {
                return Physics.OverlapBox(boxCheckAreaNoGround.position, boxCheckAreaNoGround.localScale / 2, Quaternion.identity, groundMaskID).Length <= 0;
            }
        }
    }
}
