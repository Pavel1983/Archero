using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Archero.Constants;
using Archero.InputSystem;
using Archero.UnitSystem;
using UnityEngine;

namespace Archero.Logic
{
    public class PlayerViewController : MonoBehaviour
    {
        #region life cycle

        private void OnEnable()
        {
            if (inputSystem == null)
                return;

            inputSystem.EventKeyDown += OnKeyDown;
            inputSystem.EventKeyUp += OnKeyUp;
        }

        private void OnDisable()
        {
            if (inputSystem != null)
            {
                Debug.LogError("InputSystem not set");
                return;
            }
            
            inputSystem.EventKeyDown -= OnKeyDown;
            inputSystem.EventKeyUp -= OnKeyUp;
        }

        #endregion
        #region Events
        private void OnKeyDown(EGameKeyCode key)
        {
            StopCoroutines();
            DoMove(key);
        }
        
        private void OnKeyUp(EGameKeyCode obj)
        {
           ShootClosestEnemy();
        }
        #endregion

        // setup dependencies
        public void Setup(IInputSystem inputSystem, Unit model, ILevel level)
        {
            this.inputSystem = inputSystem;
            this.model = model;
            curLevel = level;

            speed = (float)model.GetStatValue(GameConstants.UnitSpeedBaseId);
            reloadTime = (float)model.GetStatValue(GameConstants.UnitReloadTimeId);
            
            inputSystem.EventKeyDown += OnKeyDown;
            inputSystem.EventKeyUp += OnKeyUp;
        }

        private void StopCoroutines()
        {
            if (waitForShootRoutine != null)
                StopCoroutine(waitForShootRoutine);
            
            if (waitForWeaponReload != null)
                StopCoroutine(waitForWeaponReload);
        }

        private void DoMove(EGameKeyCode key)
        {
            switch (key)
            {
                case EGameKeyCode.KeyCode_Left:
                    transform.position += Vector3.left * Time.deltaTime * speed;
                    break;
                case EGameKeyCode.KeyCode_Right:
                    transform.position += Vector3.right * Time.deltaTime * speed;
                    break;
                case EGameKeyCode.KeyCode_Up:
                    transform.position += Vector3.forward * Time.deltaTime * speed;
                    break;
                case EGameKeyCode.KeyCode_Down:
                    transform.position -= Vector3.forward * Time.deltaTime * speed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }

        #region Shooting
        private void ShootClosestEnemy()
        {
            // Поиск ближайшего врага
            IReadOnlyCollection<UnitViewController> enemies = curLevel.GetEmemies();
            var closestEnemy = GetClosestEnemy(enemies);
            waitForShootRoutine = StartCoroutine(Wait(delayBeforeShoot, () =>
            {
                // стрельба
                waitForWeaponReload = StartCoroutine(Shooting(reloadTime, closestEnemy.transform));
            }));
        }
        
        private UnitViewController GetClosestEnemy(IReadOnlyCollection<UnitViewController> enemies)
        {
            if (enemies.Count == 0)
                return null;
            
            var sorted = enemies.
                OrderBy(i => (i.transform.position - transform.position).sqrMagnitude).ToList();

            return sorted[0];
        }

        private IEnumerator Wait(float duration, Action onFinished)
        {
            yield return new WaitForSeconds(duration);
            
            onFinished?.Invoke();
        }

        private void Shoot(Transform target)
        {
            var simple = Instantiate(startBulletConfig.Prefab);
            simple.transform.position = transform.position;
            var bullet = simple.GetComponent<BulletSimple>();
            var bulletDir = (target.position - transform.position);
            bulletDir.y = 0.0f;
            bullet.Setup(bulletDir.normalized, bulletSpeed, bulletDamage);
            bullet.MakeActive(true);
        }

        private IEnumerator Shooting(float reloadTime, Transform target)
        {
            while (true)
            {
                Shoot(target);
                
                yield return new WaitForSeconds(reloadTime);
            }
        }
        #endregion

        #region vars

        [SerializeField] private float delayBeforeShoot = 0.2f;
        [SerializeField] private BulletConfig startBulletConfig;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletDamage;
        
        private IInputSystem inputSystem;
        private Unit model;
        private float speed;
        private float reloadTime;
        private ILevel curLevel;
        private Coroutine waitForShootRoutine;
        private Coroutine waitForWeaponReload;

        #endregion

    }
}
