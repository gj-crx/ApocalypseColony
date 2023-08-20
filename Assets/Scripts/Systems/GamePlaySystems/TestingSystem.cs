using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Testing
{
    public class TestingSystem : GameSystem, ISystem
    {
        public TestingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
    }
}
