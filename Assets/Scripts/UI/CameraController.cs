using UnityEngine;

namespace Frogi {
    public class CameraController : MonoBehaviour {
        private const float _SMOOTH_SPEED = 0.03f;
        
        private readonly Vector3 _offset = new Vector3(0, 0, -40);
        private Transform _target;

        private void Start() => _target = FindObjectOfType<PlayerAvatar>().transform;

        private void LateUpdate() => FollowTarget();

        private void FollowTarget() {
            var desiredPosition = _target.position + _offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _SMOOTH_SPEED);
            transform.position = smoothedPosition;
        }
    }
}
