using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units {
    [System.Serializable]
    public class UnitAttackingComponent : IUnitComponent
    {
        [HideInInspector] public Unit CurrentTarget = null;
        public float CurrentAttackDelay
        {
            get { return currentAttackDelayMiliseconds; }
            set { currentAttackDelayMiliseconds = (int)(value * 1000); }
        }

        private int currentAttackDelayMiliseconds = 0;

        public void CopyStatsFrom(IUnitComponent from)
        {
            var copyFrom = from as UnitAttackingComponent;

        }
    }
}
