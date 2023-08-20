using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Systems.Pathfinding
{
    public interface IPathfinding
    {
        Vector3[] GetWayPath(Vector3 From, Vector3 Target, IBodyType bodyType, byte MaximumCorrectionStep = 2);
    }
}
