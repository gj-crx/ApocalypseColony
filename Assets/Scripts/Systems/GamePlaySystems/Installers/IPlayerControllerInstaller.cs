using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Systems.Installers
{
    public interface IPlayerControllerInstaller
    {
        void Resolve(GameSystemsManager systemsManager);
    }
}
