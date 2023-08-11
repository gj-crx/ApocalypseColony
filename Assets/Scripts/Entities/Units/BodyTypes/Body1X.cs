using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    [System.Serializable]
    public class Body1X : IBodyType
    {
        public byte BodyRadius
        {
            get { return 1; }
        }

        public bool CheckBodyForm(Vector2Int positionToCheck, Func<Vector2Int, bool> passablePathChecking)
        {
            return passablePathChecking(positionToCheck);
        }

        public static IBodyType GetBodyType(BodyType selectedType)
        {
            if (selectedType == BodyType.body1X) return new Body1X();
            if (selectedType == BodyType.body2XTop) return new Body2X();
            if (selectedType == BodyType.body4X) return new Body4X();
            if (selectedType == BodyType.body6X) return new Body6X();
            if (selectedType == BodyType.body9X) return new Body9X();

            return new Body1X();
        }
    }
}