using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ClientSideLogic;

namespace Factions.ClientSide
{
    public class FactionRepresentation : MonoBehaviour
    {
        public short FactionID = -1;
        public float[] Resources = new float[5];

        public static void CreateFactionRepresentationOnAClient(Faction.FactionSeriazableData data)
        {
            FactionRepresentation newFactionRepresentation =
               GameObject.Instantiate(PrefabManager.FactionPrefab, Vector3.zero, Quaternion.identity).GetComponent<FactionRepresentation>();

            newFactionRepresentation.FactionID = data.FactionID;
            newFactionRepresentation.Resources = data.Resources;

            ClientDataBase.Singleton.AddEntityToDataBase(newFactionRepresentation);
        }
        public void ApplyData(Faction.FactionSeriazableData dataToApply)
        {
            Resources = dataToApply.Resources;
        }
    }
}
