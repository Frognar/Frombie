using System.Collections.Generic;
using UnityEngine;

namespace Frogi {
    public class TargetFinder : MonoBehaviour {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _targets;

        public ITarget FindNearestTarget() {
            var targets = FindAllTargetsInRange();
            if (NoTargetsInRange(targets)) return default;
            return GetNearestTarget(targets);
        }

        private List<ITarget> FindAllTargetsInRange() {
            var foundedTargets = Physics2D.CircleCastAll(transform.position, _range, Vector2.zero, 1f, _targets);
            var targets = new List<ITarget>();

            foreach (var target in foundedTargets) {
                if (target.transform.TryGetComponent<ITarget>(out var targetInterface)) targets.Add(targetInterface);
            }

            return targets;
        }

        private ITarget GetNearestTarget(in List<ITarget> targetsInRange) {
            var nearestTarget = targetsInRange[0];
            var distanceToCurrentTarget = CalculateDistanceTo(nearestTarget);
            for(var i = 1; i < targetsInRange.Count; i++) {
                var distanceToNewTarget = CalculateDistanceTo(targetsInRange[i]);
                if(distanceToNewTarget <= distanceToCurrentTarget) {
                    nearestTarget = targetsInRange[i];
                    distanceToCurrentTarget = distanceToNewTarget;
                }
            }
            return nearestTarget;
        }

        private static bool NoTargetsInRange(in List<ITarget> targets) => targets.Count == 0;

        private float CalculateDistanceTo(ITarget target) => (target.Position - (Vector2)transform.position).sqrMagnitude;
    }
}
