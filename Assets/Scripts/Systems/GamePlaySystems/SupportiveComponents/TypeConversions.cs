using System;
using System.Collections.Generic;
using UnityEngine;

public static class TypeConversions
{
    public static Vector3 ToVector3(Vector2Int Vector, float ZCord = 0)
    {
        return new Vector3(Vector.x, Vector.y, ZCord);
    }
    public static Vector3Int ToVector3Int(Vector2Int Vector)
    {
        return new Vector3Int(Vector.x, Vector.y, 0);
    }
    public static Vector3Int ToVector3Int(Vector3 Vector)
    {
        return new Vector3Int((int)Vector.x, (int)Vector.y, (int)Vector.z);
    }
    public static Vector2Int ToVector2Int(Vector3 sourceVector, bool useZCord)
    {
        if (useZCord) return new Vector2Int((int)sourceVector.x, (int)sourceVector.z);
        else return new Vector2Int((int)sourceVector.x, (int)sourceVector.y);
    }
    public static Vector2Int ToVector2Int(Vector3Int v)
    {
        return new Vector2Int(v.x, v.y);
    }

    public static Vector2Int NormalizeVector2Int(Vector2Int referenceVector)
    {
        if (referenceVector.x > 0) referenceVector.x = 1;
        if (referenceVector.y > 0) referenceVector.y = 1;
        if (referenceVector.x < 0) referenceVector.x = -1;
        if (referenceVector.y < 0) referenceVector.y = -1;
        return referenceVector;
    }
    public static Vector3 GetDirectionBetween2Points(Vector3 From, Vector3 To)
    {
        Vector3 delta = To - From;
        float cordSum = Math.Abs(delta.x) + Math.Abs(delta.y);
        delta.x /= cordSum;
        delta.y /= cordSum;
        return delta;
        
    }

    public static bool ArrayOfBoolEquals(bool[] b1, bool[] b2)
    {
        if (b1.Length != b2.Length) return false;
        for (int i = 0; i < b1.Length; i++)
        {
            if (b1[i] != b2[i]) return false;
        }
        return true;
    }
    public static Vector2Int ToUnitVector(Vector2Int v)
    {
        if (v.x > 0) v.x = 1;
        if (v.x < 0) v.x = -1;

        if (v.y > 0) v.y = 1;
        if (v.y < 0) v.y = -1;

        return v;
    }
    public static Vector3Int ToUnitVector(Vector3 v)
    {
        if (v.x > 0) v.x = 1;
        if (v.x < 0) v.x = -1;

        if (v.y > 0) v.y = 1;
        if (v.y < 0) v.y = -1;

        return ToVector3Int(v);
    }
    public static Vector3[] ConvertToVector3Array(Vector2Int[] SourceArray, float AddictiveValue = 0, bool useZCord = false)
    {
        Vector3[] NewArray = new Vector3[SourceArray.Length];
        for (int i = 0; i < SourceArray.Length; i++)
        {
            if (useZCord) NewArray[i] = new Vector3(SourceArray[i].x, 0, SourceArray[i].y) + new Vector3(AddictiveValue, 0, AddictiveValue);
            else NewArray[i] = ToVector3(SourceArray[i]) + new Vector3(AddictiveValue, AddictiveValue);
        }
        return NewArray;
    }
    public static int BoolToInt(bool b)
    {
        if (b) return 1;
        else return 0;
    }

    public static Vector2 AngleToVector2(float degree)
    {
        return new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
    }
    public static float DirectionToAngle(Vector3 Direction)
    {
        if (Mathf.Abs(Direction.x) > Mathf.Abs(Direction.y))
        {
            if (Direction.x > 0) return -90;
            else if (Direction.x < 0) return 90;
        }
        else
        {
            if (Direction.y > 0) return 0;
            else if (Direction.y < 0) return 180;
        }
        return 0;
    }
    public static Vector3 RemoveZCord(Vector3 referreceVector3, float zCord = 0)
    {
        return new Vector3(referreceVector3.x, referreceVector3.y, zCord);
    }
    public static Vector2Int ReverseDirection(Vector2Int direction)
    {
        if (direction.x != 0) return new Vector2Int(0, direction.x);
        if (direction.y != 0) return new Vector2Int(direction.y, 0);
        return Vector2Int.zero;
    }
    public static Vector3Int ReverseDirection(Vector2Int direction, bool vector3Output)
    {
        if (direction.x != 0) return new Vector3Int(0, direction.x);
        if (direction.y != 0) return new Vector3Int(direction.y, 0);
        return Vector3Int.zero;
    }
}
