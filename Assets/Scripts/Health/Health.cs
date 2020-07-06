using System;
using Unity.Mathematics;

namespace Frogi {
    public class Health {
        public event Action<float> OnHealthChange = delegate {  };

        public long CurrentHealth { get; private set; }
        public long MaxHealth { get; private set; }
        public bool Dead => CurrentHealth <= 0;

        public void ModifyHealth(int amount) {
            CurrentHealth += amount;
            CurrentHealth = math.clamp(CurrentHealth, 0, MaxHealth);

            var healthPercentage = (float)CurrentHealth / MaxHealth;
            OnHealthChange(healthPercentage);
        }

        public void UpgradeHealth(int amount) {
            if (amount < 0) return;
            
            MaxHealth += amount;
            CurrentHealth += amount;

            var healthPercentage = (float)CurrentHealth / MaxHealth;
            OnHealthChange(healthPercentage);
        }

        public void SetHealth(int maxHealth) {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
    }
}
