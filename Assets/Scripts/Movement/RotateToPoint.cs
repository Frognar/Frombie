using UnityEngine;

namespace Frogi {
    public class RotateToPoint : MonoBehaviour {
        [SerializeField] private Transform _objectToRotate;

        public Transform _rotatedTransform => _objectToRotate;
        
        public void Rotate(Vector2 pointToRotate) {
            var lookDirection = pointToRotate - (Vector2)_objectToRotate.position;
            var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            _objectToRotate.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}