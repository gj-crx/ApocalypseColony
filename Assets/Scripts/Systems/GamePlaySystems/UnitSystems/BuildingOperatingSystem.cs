using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Units;
using Zenject;

namespace Systems
{
    public class BuildingOperatingSystem : GameSystem, ISystem
    {

        public BuildingOperatingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
    }
}
