using UnityEngine;

namespace Archero.Logic
{
    public interface IUnitAI
    {
        void Activate(GameObject objectToControl);
        void Deactivate();
    }
}
