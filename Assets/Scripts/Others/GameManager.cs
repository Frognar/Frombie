using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Frogi {
    public class GameManager : MonoBehaviour {
        public static event Action OnWin = delegate { };

        [SerializeField] private AudioMixer _audioMixer;

        private int _spawnersToKill;
        private int _killedSpawners;
        
        private void Awake() => _audioMixer.SetFloat("MasterVolume", 0f);
        
        private void Start() {
            PlayerAvatar.OnDeath += BackToMenu;
            MonsterSpawner.OnSpawnerKilled += ReactToSpawnedDeath;
            _spawnersToKill = FindObjectsOfType<MonsterSpawner>().Length;
        }

        private void OnDisable() => PlayerAvatar.OnDeath -= BackToMenu;

        private void ReactToSpawnedDeath() {
            _killedSpawners++;
            if (_killedSpawners == _spawnersToKill) EndGame();
        }

        private void EndGame() {
            OnWin();
            BackToMenu();
        }

        private void BackToMenu() {
            _audioMixer.SetFloat("MasterVolume", -80f);
            StartCoroutine(ResetGameCoroutine());
        }

        private IEnumerator ResetGameCoroutine() {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
