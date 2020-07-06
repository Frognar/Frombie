using System;

namespace Frogi {
    public interface IWeapon {
        event Action OnShot;
        bool CanAttack { get; }
    }
}
