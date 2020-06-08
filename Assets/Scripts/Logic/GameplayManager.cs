using System.Collections.Generic;
using Archero.Constants;
using Archero.InputSystem;
using Archero.UnitSystem;
using UnityEngine;

namespace Archero.Logic
{
    public class GameplayManager : MonoBehaviour
    {
        #region life cycle

        private void Start()
        {
            // временно
            unitFactory = new UnitFactory();

            var spawnPointTransform = currentLevel.GetPlayerSpawnPoint();
            var player = CreatePlayerOnLevel(inputObject.GetComponent<IInputSystem>(), currentLevel);
            player.transform.position = spawnPointTransform.position;

            inputSystem = inputObject.GetComponent<IInputSystem>();
            
            currentLevel.ActivateLevel();
        }

        #endregion

        public void Setup()
        {
            
        }

        private PlayerViewController CreatePlayerOnLevel(IInputSystem inputSystem, ILevel level)
        {
            List<UnitStat> stats = new List<UnitStat>();
            stats.Add(new UnitStat(GameConstants.HealthId, 100.0f));
            stats.Add(new UnitStat(GameConstants.UnitSpeedBaseId, 10.0f));
            stats.Add(new UnitStat(GameConstants.BulletSpeedBaseId, 20.0f));
            stats.Add(new UnitStat(GameConstants.UnitReloadTimeId, 0.4f));
            
            var player = unitFactory.CreateUnit("Player", stats);

            var ret = Instantiate(playerPrefab);
            
            ret.Setup(inputSystem, player, level);

            return ret;
        }

        #region vars

        [SerializeField] private Level currentLevel;
        [SerializeField] private PlayerViewController playerPrefab;
        [SerializeField] private GameObject inputObject;
        

        private IUnitFactory unitFactory;
        private IInputSystem inputSystem;
        private PlayerViewController playerViewController;


        #endregion
    }
}    
