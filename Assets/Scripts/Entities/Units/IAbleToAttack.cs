using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public interface IAbleToAttack
    {
        Unit CurrentTarget { get; set; }
    }
}
