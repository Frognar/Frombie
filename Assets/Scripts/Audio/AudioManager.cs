using System;
using UnityEngine;

namespace Frogi.Audio {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {
        private static AudioManager _instance;
        public static AudioManager Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<AudioManager>();

                    if (_instance == null) 
                        _instance = new GameObject(nameof(AudioManager)).AddComponent<AudioManager>();
                }

                return _instance;
            }
        }
        
        [SerializeField] private Sound _music;
        [SerializeField] private Sound[] _sounds;

        private AudioSource _musicSource;

        private void Awake() {
            if (_instance != null) Destroy(gameObject);
            
            _instance = this;

            _musicSource = GetComponent<AudioSource>();
            _music.SetUpAudioSource(_musicSource);
        }

        private void Start() => PlayMusic();

        public void PlaySoundEffectOneShotOnSource(SoundNameEnum soundName, AudioSource audioSource) {
            var soundEffect = FindSound(soundName);
            var clip = soundEffect.GetRandomClip();
            audioSource.PlayOneShot(clip, soundEffect.Volume);
        }

        public void PlaySoundEffectOnSource(SoundNameEnum soundName, AudioSource audioSource) {
            var soundEffect = FindSound(soundName);
            var clip = soundEffect.GetRandomClip();
            soundEffect.SetUpAudioSource(audioSource);
            audioSource.clip = clip;
            audioSource.Play();
        }
        
        private void PlayMusic() {
            var music = _music?.GetRandomClip();
            _musicSource.clip = music;
            _musicSource.Play();
        }

        private Sound FindSound(SoundNameEnum soundName) => Array.Find(_sounds, sound => sound.Name == soundName);
    }
}
