using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodyTypes
{
    public interface IBodyType
    {
        byte BodyRadius { get; }
        bool CheckBodyForm(Vector2Int positionToCheck, Func<Vector2Int, bool> passablePathChecking);

    }
    public enum BodyType
    {
        body1X,
        body2XTop,
        body4X,
        body6X,
        body9X
    }
}
