using UnityEngine;

namespace Frogi {
    public class Laser : Shooter {
        [SerializeField] private LayerMask _targets;
        
        private bool _playingVisuals;

        protected override void Awake() {
            base.Awake();
            _playingVisuals = false;
        }

        private void Update() {
            _rotateToPoint.Rotate(PointInWorld);
            
            if(ShotWasTriggered) {
                var target = RayCastTarget();
                _particlePlayer.UpdateLifeTime(target.distance - .7f);
                StartVisuals();
                if (CanShot)
                    DoDamage(target.transform);
            }
            else StopVisuals();
        }

        private void StartVisuals() {
            if (_playingVisuals) return;
            
            _playingVisuals = true;
            _particlePlayer.Play();
            _singleSoundEffectPlayer.Play();
        }

        private void StopVisuals() {
            if (!_playingVisuals) return;
            
            _playingVisuals = false;
            _particlePlayer.Stop();
            _singleSoundEffectPlayer.Stop();
        }

        private RaycastHit2D RayCastTarget() => 
            Physics2D.Raycast(transform.position, _rotateToPoint._rotatedTransform.up, 15f, _targets);

        private void DoDamage(Transform target) {
            if (target == null) return;
            _nextShotTime = Time.time + 1f / _shotsPerSecond;
            if (target.CompareTag("Monster")) target.GetComponent<ITakeDamage>().TakeDamage(_damage);
        }
    }
}