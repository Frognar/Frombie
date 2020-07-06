using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Frogi {
    public class MenuManager : MonoBehaviour {
        [SerializeField] private AudioMixer _audioMixer;
        private void Awake() => _audioMixer.SetFloat("MasterVolume", 0f);
        public void Play() => SceneManager.LoadScene(1, LoadSceneMode.Single);
        public void Quit() => Application.Quit();
    }
}
