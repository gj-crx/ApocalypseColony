using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public interface ISynchronizableObject
{
    IEntityData GetDataToTransfer();
    void ApplyTransferedData(IEntityData transferedData);
}
