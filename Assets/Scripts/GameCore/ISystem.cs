using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem 
{
    void SystemIterationCycle(int customTimeInterval = -1);
    bool IsActive { set; }
}
