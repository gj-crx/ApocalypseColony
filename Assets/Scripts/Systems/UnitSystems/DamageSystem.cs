using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;

public static class DamageSubSystem
{
    public static void UnitsHitsSomething(Unit attacker, Unit victim, float timeIntervalSeconds)
    {
        if (attacker.ComponentAttacking == null) return;

        if (attacker.ComponentAttacking.CurrentAttackDelay <= 0)
        { //Ready to hit
            if (Vector3.Distance(attacker.Position, victim.Position) < attacker.AttackRange)
            { //Target in range
                victim.CurrentHP -= attacker.Damage;

                //Delay after each hit
                attacker.ComponentAttacking.CurrentAttackDelay = attacker.AttackInterval;
            }
        }
        else
        { //wait before next hit
            attacker.ComponentAttacking.CurrentAttackDelay -= timeIntervalSeconds;
        }
    }
}