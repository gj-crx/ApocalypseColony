using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodyTypes
{
    public class Body9X : IBodyType
    {
        public byte BodyRadius
        {
            get { return 2; }
        }

        public bool CheckBodyForm(Vector2Int positionToCheck, Func<Vector2Int, bool> passablePathChecking)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (passablePathChecking(positionToCheck + new Vector2Int(x, y)) == false) return false;
                }
            }
            return true;
        }
    }
}
