using UnityEngine;

namespace Frogi {
    public class Bullet : MonoBehaviour {
        private const float _LIFE_TIME = 2f;
        [SerializeField] private float _bulletSpeed;
        
        private float _timeFromSpawn;
        private int _damage = 25;

        private bool TimeEnd => (_timeFromSpawn += Time.deltaTime) >= _LIFE_TIME;

        private void OnEnable() => _timeFromSpawn = 0f;

        private void Update() {
            MoveBullet();
            if (TimeEnd) ReturnBulletToPool();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (MonsterWasHitted(other.gameObject)) {
                DoDamage(other.gameObject);
                ReturnBulletToPool();
            }
        }

        public void SetDamage(int damage) => _damage = damage;

        private void MoveBullet() => transform.position += transform.up * _bulletSpeed * Time.deltaTime;

        private static bool MonsterWasHitted(GameObject hittedObject) => hittedObject.CompareTag("Monster");
        
        private void DoDamage(GameObject hittedObject) {
            if(hittedObject.TryGetComponent<ITakeDamage>(out var damageable)) damageable.TakeDamage(_damage);
        }

        private void ReturnBulletToPool() => BulletPool.Instance.ReturnToPool(this);
    }
}
