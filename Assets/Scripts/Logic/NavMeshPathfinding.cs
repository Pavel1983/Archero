using UnityEngine;
using UnityEngine.AI;

namespace Archero.Logic
{
    public class NavMeshPathfinding : IPathfinding
    {
        public Vector3[] GetPathTo(Vector3 source, Vector3 dest)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.SamplePosition(source, out var sourceHit, 1, NavMesh.AllAreas);
            NavMesh.SamplePosition(dest, out var destHit, 1, NavMesh.AllAreas);
            
            NavMesh.CalculatePath(sourceHit.position, destHit.position, NavMesh.AllAreas, path);

            return path.corners;
        }
    }
}
