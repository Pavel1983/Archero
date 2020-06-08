using System;
using Archero.Constants;
using Archero.Logic;
using UnityEngine;

namespace Archero.UnitSystem
{
    public class UnitViewController : MonoBehaviour
    {
        public event Action EventHit;
        public event Action EventDie;
        
        public Unit Model => model;
        
        public void Setup(Unit model)
        {
            this.model = model;
            // некогда думать.. говнокод
            // todo: подумать над лучшим решением
            var healthStat = model.GetStatValue(GameConstants.HealthId);
            if (healthStat == null)
                Debug.LogError($"{GameConstants.HealthId} stat not found");
            else
                health = (float)healthStat;
        }

        public virtual void Activate()
        {
            active = true;
        }

        public virtual void Deactivate()
        {
            active = false;
        }

        public bool IsActive()
        {
            return active;
        }

        // todo: перенести в другое место
        protected void TakeDamage(float damage)
        {
            health -= damage;
            model.SetStatValue(GameConstants.HealthId, health);
            EventHit?.Invoke();
            if (health <= 0.0f)
            {
                EventDie?.Invoke();
                // поменяется в будущем
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("bullet"))
            {
                BulletSimple bulletSimple = other.gameObject.GetComponent<BulletSimple>();
                if (bulletSimple)
                {
                    TakeDamage(bulletSimple.Damage);
                }
            }
        }

        #region vars

        private Unit model;
        private bool active;

        private float health;

        #endregion
    }
}
