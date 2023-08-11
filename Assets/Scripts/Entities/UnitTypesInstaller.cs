using System.Collections.Generic;
using UnityEngine;

using Zenject;


namespace Units
{
    public class UnitTypesInstaller : MonoInstaller
    {
        [SerializeField] private List<Unit> unitTypes;

        public override void InstallBindings()
        {

        }
    }
}