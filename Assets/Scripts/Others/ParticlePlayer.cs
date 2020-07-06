using UnityEngine;

namespace Frogi {
    public class ParticlePlayer : MonoBehaviour {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Play() => _particleSystem.Play();

        public void Stop() => _particleSystem.Stop();

        public void UpdateLifeTime(float lifeTime) => _particleSystem.startLifetime = lifeTime;
    }
}
