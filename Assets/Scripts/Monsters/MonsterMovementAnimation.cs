using Pathfinding;
using UnityEngine;

namespace Frogi {
    public class MonsterMovementAnimation : MonoBehaviour, IMovementDirection {
        private AIPath _aiPath;

        public Vector2 MovementDirection { get; private set; }
        
        private void Awake() => _aiPath = GetComponentInParent<AIPath>();

        private void Update() => MovementDirection = _aiPath.targetDirection.normalized;
    }
}
