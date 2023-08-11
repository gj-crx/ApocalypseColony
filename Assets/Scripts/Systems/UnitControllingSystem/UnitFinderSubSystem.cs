using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

namespace Systems.UnitAI
{
    public static class UnitFinderSubSystem
    {
        public static Unit FindClosestUnitInRange(Unit lookingUnit, float searchDistance, List<Unit> unitSampleToSearchIn, out float distanceToUnit)
        {
            float minDistance = searchDistance;
            Unit minDistanceUnit = null;

            foreach (var unit in unitSampleToSearchIn)
            {
                float currentRange = Vector3.Distance(unit.Position, lookingUnit.Position);

                if (currentRange < minDistance && unit != lookingUnit)
                {
                    minDistance = currentRange;
                    minDistanceUnit = unit;
                }
            }

            distanceToUnit = minDistance;
            return minDistanceUnit;
        }
    }
}
