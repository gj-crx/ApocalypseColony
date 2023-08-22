using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using Units;

namespace Factions
{
    [System.Serializable]
    public class Faction
    {

        public short FactionID = 0;
        public ulong ControllingPlayerID = 0;
        public Game AssociatedGame;

        public float[] Resources = new float[5];

        public List<Unit> Buildings = new List<Unit>();


        public Faction(bool canBeCreatedOnlyBySpawnerSystem)
        { 
           
        }

        /// <summary>
        /// Contains all the information that has to be transfered to clients
        /// </summary>
        [System.Serializable]
        public struct FactionSeriazableData : INetworkSerializable
        {
            public short FactionID;
            public float[] Resources;

            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref FactionID);
                serializer.SerializeValue(ref Resources);
            }
        }
    }
}
