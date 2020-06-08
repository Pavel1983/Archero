using System;
using System.Collections.Generic;
using Archero.UnitSystem;
using UnityEngine;

namespace Archero.Logic
{
    public interface ILevel
    {
        event Action<bool> EventLevelEnded;
        Transform GetPlayerSpawnPoint();
        void ActivateLevel();
        IReadOnlyCollection<UnitViewController> GetEmemies();
    }
}
