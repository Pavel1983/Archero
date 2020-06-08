using System;
using System.Collections.Generic;
using System.Linq;
using Archero.UnitSystem;
using UnityEngine;

namespace Archero.Logic
{
    public class Level : MonoBehaviour, ILevel
    {
        #region life cylce

        private void Awake()
        {
            enemies = enemiesRoot.GetComponentsInChildren<UnitViewController>().ToList();
        }

        #endregion
        #region ILevel implementation
        public event Action<bool> EventLevelEnded;
        public Transform GetPlayerSpawnPoint()
        {
            return spawnPoint;
        }

        public void ActivateLevel()
        {
            foreach (var enemy in enemies)
            {
                enemy.Activate();
            }
        }

        public IReadOnlyCollection<UnitViewController> GetEmemies()
        {
            return enemies;
        }

        #endregion

        #region vars

        [SerializeField] private Transform enemiesRoot;
        [SerializeField] private Transform spawnPoint;

        private List<UnitViewController> enemies;

        #endregion
    }
}
