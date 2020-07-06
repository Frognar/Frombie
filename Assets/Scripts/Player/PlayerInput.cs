using UnityEngine;
using UnityEngine.InputSystem;

namespace Frogi {
    public class PlayerInput : MonoBehaviour, IMovementDirection, IAttackActionTrigger, IUpgradeActionsTrigger,
        IPointInWorld, IUpgradeDifficulty {
        private Inputs.Controls _controls;
        private Camera _camera;

        public Vector2 MovementDirection { get; private set; }
        public Vector2 PointInWorld { get; private set; }
        public bool UpgradeAvatarActionTriggered => Input.GetKeyDown(KeyCode.Space);
        public bool BuildTurretActionTriggered  => Input.GetKeyDown(KeyCode.Mouse1);
        public bool UpgradeTurretActionTriggered => Input.GetKeyDown(KeyCode.U);
        public bool UpgradeDifficultyTriggered => Input.GetKeyDown(KeyCode.Semicolon);
        public bool AttackActionTriggered { get; private set; }
        
        private void Awake() => _controls = new Inputs.Controls();

        private void Start() => _camera = Camera.main;

        private void Update() => OnMousePosition(_controls.Player.MousePosition.ReadValue<Vector2>());

        private void OnEnable() {
            _controls.Enable();
            _controls.Player.Movement.performed += OnMovement;
            _controls.Player.ClickLMB.performed += OnMouseLeftClick;
        }

        private void OnDisable() {
            _controls.Player.Movement.performed -= OnMovement;
            _controls.Player.ClickLMB.performed -= OnMouseLeftClick;
            _controls.Disable();
        }

        private void OnMovement(InputAction.CallbackContext context) =>
            MovementDirection = context.ReadValue<Vector2>();

        private void OnMousePosition(Vector2 pointOnScreen) =>
            PointInWorld = _camera.ScreenToWorldPoint(pointOnScreen);
        
        private void OnMouseLeftClick(InputAction.CallbackContext context) =>
            AttackActionTriggered = context.ReadValue<float>() >= 0.15f;
    }
}