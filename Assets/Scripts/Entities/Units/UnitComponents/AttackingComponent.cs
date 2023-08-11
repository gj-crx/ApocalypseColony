using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units {
    [System.Serializable]
    public class AttackingComponent
    {
        [HideInInspector] public Unit CurrentTarget = null;
        public float CurrentAttackDelay
        {
            get { return currentAttackDelayMiliseconds; }
            set { currentAttackDelayMiliseconds = (int)(value * 1000); }
        }

        private int currentAttackDelayMiliseconds = 0;
    }
}
