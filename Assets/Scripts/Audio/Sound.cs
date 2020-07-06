using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Frogi.Audio {
    [Serializable]
    public class Sound {
        [SerializeField] private SoundNameEnum _name;
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] [Range(0f, 1f)] private float _volume;
        [SerializeField] [Range(-3f, 3f)] private float _pitch;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        public SoundNameEnum Name => _name;
        public float Volume => _volume;
        private int RandomIndex => UnityEngine.Random.Range(0, _audioClips.Length - 1);

        public void SetUpAudioSource(AudioSource audioSource) {
            audioSource.volume = _volume;
            audioSource.pitch = _pitch;
            audioSource.loop = _loop;
            audioSource.playOnAwake = _playOnAwake;
            audioSource.outputAudioMixerGroup = _audioMixerGroup;
        }

        public AudioClip GetRandomClip() => _audioClips[RandomIndex];
    }
}
