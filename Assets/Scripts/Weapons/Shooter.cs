using Frogi.Audio;
using UnityEngine;

namespace Frogi {
    public class Shooter : MonoBehaviour {
        protected RotateToPoint _rotateToPoint;
        protected ParticlePlayer _particlePlayer;
        protected SingleSoundEffectPlayer _singleSoundEffectPlayer;
        private IProjectileLauncher _projectileLauncher;
        private IPointInWorld _pointInWorld;
        private IAttackActionTrigger _attackActionTrigger;
        protected float _nextShotTime;
        protected int _damage;
        protected float _shotsPerSecond;

        public float DamagePerSecond => _damage * _shotsPerSecond;
        protected Vector2 PointInWorld => _pointInWorld.PointInWorld;
        protected bool ShotWasTriggered => _attackActionTrigger.AttackActionTriggered;
        protected bool CanShot => Time.time >= _nextShotTime;

        protected virtual void Awake() {
            _pointInWorld = GetComponentInParent<IPointInWorld>();
            _attackActionTrigger = GetComponentInParent<IAttackActionTrigger>();
            _projectileLauncher = GetComponent<IProjectileLauncher>();
            _rotateToPoint = GetComponent<RotateToPoint>();
            _particlePlayer = GetComponent<ParticlePlayer>();
            _singleSoundEffectPlayer = GetComponent<SingleSoundEffectPlayer>();
            _damage = 5;
            _shotsPerSecond = 2f;
        }

        private void Update() {
            _rotateToPoint.Rotate(PointInWorld);
            
            if(ShotWasTriggered)
                if(CanShot)
                    Shoot();
        }

        public void UpgradeShotsPerSecond(float amount) {
            if (amount > 0f) _shotsPerSecond += amount;
        }

        public void UpgradeShotsDamage(int amount) {
            if (amount > 0) _damage += amount;
        }

        private void Shoot() {
            _projectileLauncher.Shoot(_damage);
            _particlePlayer.Play();
            _singleSoundEffectPlayer.Play();
            _nextShotTime = Time.time + 1f / _shotsPerSecond;
        }
    }
}
