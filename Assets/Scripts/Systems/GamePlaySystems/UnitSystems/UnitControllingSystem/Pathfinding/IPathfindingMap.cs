using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public interface IPathfindingMap
    {
        bool PathExistAndPassable(int XCord, int YCord);
        void AddNewPoint(int x, int y, PassageType passageType);
    }
}
