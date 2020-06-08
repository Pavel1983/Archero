using System.Collections.Generic;
using Archero.UnitSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace Archero.Logic
{
    public class EnemyPatrolBehaviour : MonoBehaviour, IUnitAI
    {
        public void Setup(List<Transform> patrolPoints)
        {
            this.patrolPoints = patrolPoints;
        }

        #region IStatemachine impl
        public void Activate(GameObject objectToControl)
        {
            Assert.IsTrue(patrolPoints.Count >= 2);
            _pathfinding = new NavMeshPathfinding();
            curPatrolPoint = this.transform;
            this.objectToControl = objectToControl;
            
            ChooseNextPatrolPoint();
        }

        public void Deactivate()
        {
            
        }
        #endregion
        
        #region Events
        private void OnExitPatrolState()
        {
            ChooseNextPatrolPoint();
        }
        #endregion

        private void ChooseNextPatrolPoint()
        {
            var nextPatrolPoint = GetNextPatrolPoint();
            var moveState = new MoveToStaticPointState(_pathfinding, 
                this.objectToControl, 
                curPatrolPoint.position, 
                nextPatrolPoint.position, 
                moveStateConfig);

            curPatrolPoint = nextPatrolPoint;
            if (state != null)
            {
                state.EventExitState -= OnExitPatrolState;
            }

            state = moveState;
            state.EventExitState += OnExitPatrolState;
            
            StartCoroutine(state.Execute());
        }

        private Transform GetNextPatrolPoint()
        {
            if (randomPatrolPoint)
            {
                List<Transform> filtered = patrolPoints.GetAllExcept(curPatrolPoint);
                return filtered[Random.Range(0, filtered.Count)];
            }

            int index = patrolPoints.IndexOf(curPatrolPoint);
            if (index == patrolPoints.Count - 1)
                return patrolPoints[0];
            
            return patrolPoints[index + 1];
        }

        #region vars

        [SerializeField] private bool randomPatrolPoint;
        [SerializeField] private MoveStateConfig moveStateConfig;
        
        private IUnitState state;
        private Transform curPatrolPoint;
        private IPathfinding _pathfinding;
        private Coroutine moveStateCoroutine;
        private GameObject objectToControl;  // object that we are controlling
        private List<Transform> patrolPoints;

        #endregion
    }
}
