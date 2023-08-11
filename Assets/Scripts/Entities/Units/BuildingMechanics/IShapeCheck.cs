using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Buildings
{
    public interface IShapeCheck
    {
        public bool IsValidPositionForBuilding { get; }
    }
}
