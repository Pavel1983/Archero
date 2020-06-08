using System.Collections.Generic;
using Archero.Logic;
using UnityEngine;

namespace Archero.UnitSystem
{
    public class SphereEnemyViewController : UnitViewController
    {
        private void OnDestroy()
        {
            if (IsActive())
                Deactivate();
        }

        public void Setup(IPathfinding pathfinding)
        {
            pathfindStuff = pathfinding;
        }

        public override void Activate()
        {
            base.Activate();

            var patrolBehaviour = Instantiate(patrolBehaviourPrefab, transform, false);
            patrolBehaviour.Setup(patrolPoints);
            patrolBehaviour.Activate(gameObject);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Destroy(gameObject.GetComponent<EnemyPatrolBehaviour>());
        }
        
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] private EnemyPatrolBehaviour patrolBehaviourPrefab;

        private IPathfinding pathfindStuff;
        private int currentPatrolPointIndex;
    }
}
