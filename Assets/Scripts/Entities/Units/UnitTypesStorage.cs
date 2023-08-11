using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units {
    public class UnitTypesStorage : MonoBehaviour
    {
        public static List<Unit> UnitTypes;

        [SerializeField] private List<Unit> unitTypes;
        private 

        void Start()
        {
            UnitTypes = unitTypes;
        }

        public static Unit GetUnitByClass(Unit.UnitClassification classToSearch)
        {
            foreach (var unit in UnitTypes)
            {
                if (unit != null && unit.Class == classToSearch) return unit;
            }
            Debug.LogError("No units of that class assigned " + classToSearch);
            return null;
        }
    }
}
