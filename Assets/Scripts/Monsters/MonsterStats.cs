using Unity.Mathematics;

namespace Frogi {
    public class MonsterStats {
        public long MaxHealth { get; private set; }
        public long GoldDrop { get; private set; }
        public long Damage { get; private set; }
        public double AttacksPerSecond { get; private set; }
        public int Level { get; private set; }

        public MonsterStats(){ }
        
        public MonsterStats(int maxHealth, int goldDrop, int damage, float attacksPerSecond, int level) {
            MaxHealth = maxHealth;
            GoldDrop = goldDrop;
            Damage = damage;
            AttacksPerSecond = attacksPerSecond;
            Level = level;
        }

        public void UpgradeMonsterStats() {
            AttacksPerSecond = math.clamp((AttacksPerSecond + .1f), 0f, float.MaxValue);
            GoldDrop = math.clamp(GoldDrop * 4, 0, int.MaxValue);
            MaxHealth = math.clamp(MaxHealth * 2, 0, int.MaxValue);
            Damage = math.clamp((long)(Damage * 1.5f), 0, int.MaxValue);
            Level++;
        }
    }
}