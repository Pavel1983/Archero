using System;
using System.Collections;
using Archero.UnitSystem;
using UnityEngine;

namespace Archero.Logic
{
    public class MoveToStaticPointState : IUnitState
    {
        public MoveToStaticPointState(IPathfinding pathfinding, 
                         GameObject objectToMove, 
                         Vector3 source, 
                         Vector3 dest, 
                         MoveStateConfig config)
        {
            this._pathfinding = pathfinding;
            this.objectToMove = objectToMove;
            sourcePos = source;
            destPos = dest;
            this.config = config;
        }

        #region IUnitState impl

        public event Action EventEnterState;
        public event Action EventExitState;

        public IEnumerator Execute()
        {
            EventEnterState?.Invoke();
            yield return DoMove();
            EventExitState?.Invoke();
        }

        #endregion

        private IEnumerator DoMove()
        {
            Vector3[] path = _pathfinding.GetPathTo(sourcePos, destPos);
            if (path.Length > 1)
            {
                int i = 1;
                while (i < path.Length)
                {
                    yield return Move(path[i - 1], path[i], () =>
                    {
                        i++;
                    });
                }
            }
        }

        private IEnumerator Move(Vector3 from, Vector3 to, Action onFinished)
        {
            Vector3 diff = to - from;
            float totalDistance = diff.magnitude;
            float coeff = 0.0f;
            float passedDist = 0.0f;
            while (coeff < 1.0f)
            {
                passedDist += Time.deltaTime * config.Speed;
                coeff = passedDist / totalDistance;
                Vector3 newPos = from + diff * coeff;

                objectToMove.transform.position = newPos;

                yield return null;
            }
            
            onFinished?.Invoke();
        }

        #region vars

        private Vector3 sourcePos;
        private Vector3 destPos;
        private MoveStateConfig config;
        private IPathfinding _pathfinding;
        private GameObject objectToMove;

        #endregion


    }
}
