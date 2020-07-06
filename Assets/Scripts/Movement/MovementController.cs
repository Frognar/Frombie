using UnityEngine;

namespace Frogi {
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour {
        [SerializeField] private float _movementSpeed = 8f;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private IMovementDirection _movementDirection;
        
        public Vector2 MovementDirection { get; private set; }
        private Vector2 MyPosition => _rigidbody2D.position;
        private float MovementSpeed => _movementSpeed * Time.fixedDeltaTime;

        private void Awake() => _movementDirection = GetComponentInParent<IMovementDirection>();

        private void Update() => UpdateMovementDirection();

        private void FixedUpdate() => Move();
        
        private void UpdateMovementDirection() => MovementDirection = _movementDirection.MovementDirection;
        
        private void Move() => _rigidbody2D.MovePosition(MyPosition + MovementDirection * MovementSpeed);
    }
}