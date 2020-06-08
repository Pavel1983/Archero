using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archero.Logic
{
    public interface IPathfinding
    {
        Vector3[] GetPathTo(Vector3 source, Vector3 dest);
    }
}
