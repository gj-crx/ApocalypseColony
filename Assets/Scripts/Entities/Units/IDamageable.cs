using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public interface IDamageable
    {
        void TakeDamage(float damage, Unit damageSource = null);
    }
}
