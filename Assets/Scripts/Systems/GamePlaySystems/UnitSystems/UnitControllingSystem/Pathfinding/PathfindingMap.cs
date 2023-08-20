using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pathfinding {
    public class PathfindingMap : ISystem, IPathfindingMap
    {
        private ObstaclesMap obstaclesMap = new ObstaclesMap();

        public bool IsActive { get; set; }

        public bool PathExistAndPassable(int XCord, int YCord)
        {
            if (obstaclesMap[XCord, YCord] == null) obstaclesMap[XCord, YCord] = new PassablePoint(PassageType.PassableByLand);

            return obstaclesMap[XCord, YCord].PointType == PassageType.PassableByLand;
        }
        public void AddNewPoint(int x, int y, PassageType passageType) => obstaclesMap[x, y] = new PassablePoint(passageType);

        public void SystemIterationCycle(int customTimeInterval = -1) => throw new NotImplementedException();

        public PathfindingMap()
        {

        }

        internal class ObstaclesMap
        {
            Dictionary<Tuple<int, int>, PassablePoint> ObstaclesMapDictionary = new Dictionary<Tuple<int, int>, PassablePoint>();


            public PassablePoint this[int x, int y]
            {
                get
                {
                    var t = Tuple.Create(x, y);
                    if (ObstaclesMapDictionary.ContainsKey(t)) return ObstaclesMapDictionary[t];
                    return null;
                }
                set
                {
                    var t = Tuple.Create(x, y);
                    ObstaclesMapDictionary[t] = value;
                }
            }
        }
        internal class PassablePoint
        {
            public PassageType PointType { get; set; } = PassageType.PassableByLand;
            public PassablePoint(PassageType pointType)
            {
                PointType = pointType;
            }
        }
       
    }
    public enum PassageType : byte
    {
        PassableByLand = 0,
        ObstructedPassage = 1,
        Water = 2,
        UnpassableByLand = 3
    }
}
