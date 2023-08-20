using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pathfinding
{
    public interface IPathfindingMap
    {
        bool PathExistAndPassable(int XCord, int YCord);
        void AddNewPoint(int x, int y, PassageType passageType);
    }
}
