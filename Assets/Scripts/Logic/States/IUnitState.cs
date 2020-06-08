using System;
using System.Collections;

namespace Archero.UnitSystem
{
    public interface IUnitState
    {
        event Action EventEnterState;
        event Action EventExitState;
        IEnumerator Execute();
    }
}
