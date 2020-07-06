using System;
using UnityEngine;

namespace Frogi {
    public class MonsterSpawner : MonoBehaviour, ITakeDamage, ITarget {
        public static event Action<int, int, float> OnMonsterUpgrade = delegate { };
        public static event Action OnSpawnerKilled = delegate { };

        private const float _SPAWNING_TIME = 2f;
        
        [SerializeField] private Transform _spawnPosition;
        
        private Health _health;
        private HealthBar _healthBar;
        private IUpgradeDifficulty _upgradeDifficulty;
        private MonsterStats _monsterStats;
        private float _lastSpawnTime;
        private float _localTime;

        public Vector2 Position => transform.position;
        private bool UpgradeDifficultyTriggered => _upgradeDifficulty.UpgradeDifficultyTriggered;
        private bool TimeToSpawn => _localTime >= _lastSpawnTime + _SPAWNING_TIME;
        private float MonsterDamagePerSecond => (float) (_monsterStats.Damage * _monsterStats.AttacksPerSecond);

        private void Awake() {
            _monsterStats = new MonsterStats(50, 25, 5, .5f, 0);
            _health = new Health();
            _health.SetHealth(int.MaxValue);
            _healthBar = GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth(_health);
        }

        private void Start() => _upgradeDifficulty = FindObjectOfType<PlayerInput>();

        private void Update() {
            UpdateTime();

            if (TimeToSpawn) {
                Heal();
                SpawnMonster();
                _lastSpawnTime = _localTime;
            }

            if (UpgradeDifficultyTriggered) UpgradeMonster();
        }
        
        private void UpdateTime() => _localTime += Time.deltaTime;

        private void Heal() => _health.ModifyHealth(2500);
        
        private void SpawnMonster() {
            var monster = MonsterPool.Instance.Get();
            if(HaveMonster(monster)) SetUpSpawnedMonster(monster);
        }

        private static bool HaveMonster(Monster monster) => monster != default;

        private void SetUpSpawnedMonster(Monster monster) {
            var monsterTransform = monster.transform;
            monsterTransform.position = _spawnPosition.position;
            monsterTransform.rotation = Quaternion.identity;

            monster.SetUpMonster(_monsterStats);
            monster.gameObject.SetActive(true);
        }

        private void UpgradeMonster() {
            _monsterStats.UpgradeMonsterStats();
            OnMonsterUpgrade(_monsterStats.Level, (int)_monsterStats.MaxHealth, MonsterDamagePerSecond);
        }

        public void TakeDamage(int damage) {
            _health.ModifyHealth(-damage);

            if (_health.Dead) Die();
        }

        private void Die() => OnSpawnerKilled();
    }
}
