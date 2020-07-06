using UnityEngine;

namespace Frogi {
    public class BulletLauncher : MonoBehaviour, IProjectileLauncher {
        [SerializeField] private Transform _shotOrigin;
        
        public void Shoot(int damage) {
            var bullet = BulletPool.Instance.Get();
            SetUpBullet(bullet, damage);
        }

        private void SetUpBullet(Bullet bullet, int damage) {
            var bulletTransform = bullet.transform;
            bulletTransform.position = _shotOrigin.position;
            bulletTransform.rotation = _shotOrigin.rotation;
            bullet.SetDamage(damage);
            bullet.gameObject.SetActive(true);
        }
    }
}
