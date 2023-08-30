using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public interface IUnitComponent
    {
        void CopyStatsFrom(IUnitComponent unitComponentToCopyStatsFrom);
    }
}
