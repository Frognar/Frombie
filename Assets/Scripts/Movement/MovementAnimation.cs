using UnityEngine;

namespace Frogi {
    [RequireComponent(typeof(Animator))]
    public class MovementAnimation : MonoBehaviour {
        [SerializeField] private Animator _animator;
        private IMovementDirection _movementDirection;

        // Animation Strings
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start() => _movementDirection = GetComponentInParent<IMovementDirection>();

        private void LateUpdate() => AnimateMovement(_movementDirection.MovementDirection);

        private void AnimateMovement(Vector2 movementDirection) {
            _animator.SetFloat(Horizontal, movementDirection.x);
            _animator.SetFloat(Vertical, movementDirection.y);
            _animator.SetFloat(Speed, movementDirection.sqrMagnitude);
        }
    }
}
