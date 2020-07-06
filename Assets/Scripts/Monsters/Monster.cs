using Frogi.Audio;
using Pathfinding;
using UnityEngine;

namespace Frogi {
    public class Monster : MonoBehaviour, ITarget, ITakeDamage {
        private const float _ATTACK_RANGE_SQR = 2.25f;
        
        private Health _health;
        private HealthBar _healthBar;
        private ParticlePlayer _particlePlayer;
        private SingleSoundEffectPlayer _singleSoundEffectPlayer;
        private AIDestinationSetter _aiDestinationSetter;
        private PlayerAvatar _playerAvatar;
        private MonsterStats _monsterStats;
        private float _localTime;
        private float _nextAttackTime;
        
        public Vector2 Position => transform.position;
        private bool PlayerAvatarInAttackRange => 
            (transform.position - _playerAvatar.transform.position).sqrMagnitude <= _ATTACK_RANGE_SQR;
        private bool CanAttack => _localTime >= _nextAttackTime;
        private static GoldSystem PlayerGold => PlayerController.Instance.GoldSystem;

        private void Awake() {
            _monsterStats = new MonsterStats();
            _health = new Health();
            _healthBar = GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth(_health);
            _particlePlayer = GetComponent<ParticlePlayer>();
            _singleSoundEffectPlayer = GetComponent<SingleSoundEffectPlayer>();
            _aiDestinationSetter = GetComponent<AIDestinationSetter>();
            _playerAvatar = FindObjectOfType<PlayerAvatar>();
            _aiDestinationSetter.target = _playerAvatar.transform;
        }

        private void Update() {
            UpdateLocalTime();
            
            if (PlayerAvatarInAttackRange)
                if(CanAttack)
                    Attack();
        }

        public void TakeDamage(int damage) {
            if (_health.Dead) return;
            
            _health.ModifyHealth(-damage);

            if (_health.Dead) Die();
        }

        public void SetUpMonster(MonsterStats monsterStats) {
            _monsterStats = monsterStats;
            _health.SetHealth((int)_monsterStats.MaxHealth);
        }
        
        private void UpdateLocalTime() => _localTime += Time.deltaTime;
        
        private void Die() {
            MonsterPool.Instance.ReturnToPool(this);
            PlayerGold.ModifyGold((int)_monsterStats.GoldDrop);
        }

        private void Attack() {
            if (_health.Dead) return;
            
            _nextAttackTime = _localTime + 1f / (float)_monsterStats.AttacksPerSecond;
            _particlePlayer.Play();
            _singleSoundEffectPlayer.Play();
            _playerAvatar.TakeDamage((int)_monsterStats.Damage);
        }
    }
}
