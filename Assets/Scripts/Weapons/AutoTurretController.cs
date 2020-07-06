using UnityEngine;

namespace Frogi {
    public class AutoTurretController : MonoBehaviour, IPointInWorld, IAttackActionTrigger {
        [SerializeField] private TargetFinder _targetFinder;

        public Vector2 PointInWorld { get; private set; }

        public bool AttackActionTriggered { get; private set; }

        private void Update() => ControlTurret();

        private void ControlTurret() {
            var target = _targetFinder.FindNearestTarget();
            var haveTarget = HaveTarget(target);

            PointInWorld = haveTarget ? target.Position : PointInWorld;
            AttackActionTriggered = haveTarget;
        }

        private static bool HaveTarget(ITarget target) => target != default;
    }
}
