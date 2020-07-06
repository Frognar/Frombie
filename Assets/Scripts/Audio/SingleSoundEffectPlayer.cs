using UnityEngine;

namespace Frogi.Audio {
    [RequireComponent(typeof(AudioSource))]
    public class SingleSoundEffectPlayer : MonoBehaviour {
        [SerializeField] private SoundNameEnum soundName;
        [SerializeField] private bool OneShot;
        
        private AudioSource _audioSource;
        private AudioManager _audioManager;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        private void Start() => _audioManager = AudioManager.Instance;

        public void Play() {
            if (OneShot) _audioManager.PlaySoundEffectOneShotOnSource(soundName, _audioSource);
            else _audioManager.PlaySoundEffectOnSource(soundName, _audioSource);
        }

        public void Stop() => _audioSource.Stop();
    }
}
