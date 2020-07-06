using System;
using Frogi.Audio;
using UnityEngine;

namespace Frogi {
    public class PlayerAvatar : MonoBehaviour, ITarget, ITakeDamage {
        public static event Action<int> OnAvatarCostChange = delegate { }; 
        public static event Action OnDeath = delegate { };
        public static event Action<float> OnAvatarUpgrade = delegate { };
        public static event Action<int, int> OnAvatarHealthChange = delegate { };
        
        private const float _SHOT_SPEED_UPGRADE = 0.1f;
        private const int _SHOT_DAMAGE_UPGRADE = 1;
        private const int _HEALTH_UPGRADE = 20;
        private const int _BASE_UPGRADE_COST = 50;

        [SerializeField] private Transform _gunPlace1;
        [SerializeField] private Transform _gunPlace2;
        [SerializeField] private Shooter _gunPrefab;
        
        private Health _health;
        private HealthBar _healthBar;
        private SingleSoundEffectPlayer _singleSoundEffectPlayer;
        private Shooter _gun1;
        private Shooter _gun2;
        private int _avatarLevel;
        
        public int UpgradeCost  => _BASE_UPGRADE_COST * _avatarLevel; 
        public Vector2 Position => transform.position;
        private bool HaveAllGuns => _gun2 != null;

        private void Awake() {
            _health = new Health();
            _health.SetHealth(100);
            _healthBar = GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth(_health);
            _singleSoundEffectPlayer = GetComponent<SingleSoundEffectPlayer>();
            _avatarLevel = 1;
        }

        private float DamagePerSecond {
            get {
                var dps = 0f;
                if (HaveAllGuns) dps = _gun1.DamagePerSecond * 2;
                else if (_gun1 != null) dps = _gun1.DamagePerSecond;

                return dps;
            }
        }
        
        public void TakeDamage(int damage) {
            _health.ModifyHealth(-damage);
            _singleSoundEffectPlayer.Play();

            if (_health.CurrentHealth <= 0f) Die();
            OnAvatarHealthChange((int)_health.CurrentHealth, (int)_health.MaxHealth);
        }

        public void UpgradeAvatar() {
            if (HaveAllGuns) UpgradeGuns();
            else AddGun();
            _avatarLevel++;
            _health.UpgradeHealth(_HEALTH_UPGRADE);
            InvokeEvents();
        }

        private void AddGun() {
            if (_gun1 == null) _gun1 = Instantiate(_gunPrefab, _gunPlace1);
            else _gun2 = Instantiate(_gunPrefab, _gunPlace2);
        }

        private void UpgradeGuns() {
            _gun1.UpgradeShotsPerSecond(_SHOT_SPEED_UPGRADE);
            _gun2.UpgradeShotsPerSecond(_SHOT_SPEED_UPGRADE);
            _gun1.UpgradeShotsDamage(_SHOT_DAMAGE_UPGRADE);
            _gun2.UpgradeShotsDamage(_SHOT_DAMAGE_UPGRADE);
        }

        private void Die() {
            OnDeath();
            gameObject.SetActive(false);
        }

        private void InvokeEvents() {
            OnAvatarCostChange(UpgradeCost);
            OnAvatarHealthChange((int)_health.CurrentHealth, (int)_health.MaxHealth);
            OnAvatarUpgrade(DamagePerSecond);
        }
    }
}
