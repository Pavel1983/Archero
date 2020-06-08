using UnityEngine;

namespace Archero.Logic
{
    // Поведение базовой пульки
    public class BulletSimple : MonoBehaviour
    {
        public float Damage => damage;
        public void MakeActive(bool flag)
        {
            active = flag;
        }

        public void Setup(Vector3 dir, float speed, float damage)
        {
            direction = dir;
            this.speed = speed;
            this.damage = damage;
        }

        private void Update()
        {
            if (active)
                transform.position += direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                Destroy(this.gameObject);
            }
        }

        #region vars

        private Vector3 direction;
        private float speed;
        private float damage;

        private bool active;

        #endregion
    }
}
