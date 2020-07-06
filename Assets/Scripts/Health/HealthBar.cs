using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Frogi {
    public class HealthBar : MonoBehaviour {
        [SerializeField] private Image _fillImage;

        private Health _health;
        private Camera _camera;
        private const float _UPDATE_SPEED_SECONDS = 0.2f;

        private void Start() => _camera = Camera.main;

        private void OnEnable() => _fillImage.fillAmount = 1f;

        private void LateUpdate() => transform.LookAt(_camera.transform);

        private void OnDestroy() => _health.OnHealthChange -= HandleHealthChanged;

        public void SetHealth(Health health) {
            _health = health;
            _health.OnHealthChange += HandleHealthChanged;
        }

        private void HandleHealthChanged(float healthPercentage) => StartCoroutine(UpdateBar(healthPercentage));

        private IEnumerator UpdateBar(float healthPercentage) {
            var preChangePct = _fillImage.fillAmount;
            var elapsed = 0f;

            while (elapsed < _UPDATE_SPEED_SECONDS) {
                elapsed += Time.deltaTime;
                var updatePercentage = elapsed / _UPDATE_SPEED_SECONDS;
                _fillImage.fillAmount = Mathf.Lerp(preChangePct, healthPercentage, updatePercentage);
                yield return null;
            }

            _fillImage.fillAmount = healthPercentage;
        }
    }
}