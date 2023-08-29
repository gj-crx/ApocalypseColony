using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;

namespace Systems
{
    public class UnitFightingSystem : GameSystem, ISystem
    {

        public UnitFightingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
        }

        public bool Hit(Unit attacker, Unit victim)
        {
            if (attacker.ComponentAttacking == null) return false;

            if (attacker.ComponentAttacking.CurrentAttackDelay <= 0)
            { //Ready to hit
                if (Vector3.Distance(attacker.Position, victim.Position) < attacker.AttackRange)
                { //Target in range
                    victim.CurrentHP -= attacker.Damage;

                    //Delay after each hit
                    attacker.ComponentAttacking.CurrentAttackDelay = attacker.AttackInterval;

                    return true;
                }
            }
            else
            { //wait before next hit
                attacker.ComponentAttacking.CurrentAttackDelay -= timeIntervalInSeconds;
            }
            return false;
        }
    }
}