using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public interface ISynchronizableObject
{
    object GetDataToTransfer();
    void ApplyTransferedData(object transferedData);
    ClientRpcParams GetClientRpcParams();
}
