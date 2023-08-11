using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public interface IMovableUnit
    {
        Vector3[] CurrentWay { get; set; }

        void MoveToPosition(Vector3 targetPosition);
    }
}
