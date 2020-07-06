using System;
using Unity.Mathematics;

namespace Frogi {
    public class GoldSystem {
        public static event Action<int> OnGoldChange = delegate {  };
        private long _gold;

        public GoldSystem(int startGold) {
            ModifyGold(startGold);
        }
        
        public bool HaveEnoughGold(int amount) => _gold >= amount;

        public void ModifyGold(int amount) {
            _gold += amount;
            _gold = math.clamp(_gold, 0, int.MaxValue);
            OnGoldChange((int)_gold);
        }
    }
}
