using UnityEngine;

namespace Frogi {
    public class PlayerController : MonoBehaviour {
        private static PlayerController _instance;

        public static PlayerController Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<PlayerController>();

                    if (_instance == null)
                        _instance = new GameObject(typeof(PlayerController).Name).AddComponent<PlayerController>();
                }

                return _instance;
            }
        }
        
        [SerializeField] private int _startGold;
        [SerializeField] private AutoTurretBuilder _autoTurretBuilder;
        [SerializeField] private PlayerAvatar _playerAvatar;

        private IPointInWorld _pointInWorld;
        private IUpgradeActionsTrigger _upgradeActionsTrigger;

        private bool UpgradeAvatarActionTrigger => _upgradeActionsTrigger.UpgradeAvatarActionTriggered;
        private bool BuildTurretActionTriggered => _upgradeActionsTrigger.BuildTurretActionTriggered;
        private bool UpgradeTurretActionTriggered => _upgradeActionsTrigger.UpgradeTurretActionTriggered;
        private Vector2 PointInWorld => _pointInWorld.PointInWorld;
        private int AvatarCost => _playerAvatar.UpgradeCost;
        private int BuildTurretCost => _autoTurretBuilder.BuildCost;
        private int UpgradeTurretsCost => _autoTurretBuilder.UpgradeCost;
        private bool HaveGoldForAvatar => GoldSystem.HaveEnoughGold(AvatarCost);
        private bool HaveGoldForBuildNewTurret => GoldSystem.HaveEnoughGold(BuildTurretCost);
        private bool HaveGoldForUpgradeTurrets => GoldSystem.HaveEnoughGold(UpgradeTurretsCost);
        public GoldSystem GoldSystem { get; private set; }

        private void Awake() {
            if(_instance != null) Destroy(gameObject);
        }
        
        private void Start() {
            _pointInWorld = GetComponent<IPointInWorld>();
            _upgradeActionsTrigger = GetComponent<IUpgradeActionsTrigger>();
            GoldSystem = new GoldSystem(_startGold);
            _playerAvatar.gameObject.SetActive(true);
        }

        private void Update() {
            if (UpgradeAvatarActionTrigger)
                if (HaveGoldForAvatar)
                    UpgradeAvatar();

            if (UpgradeTurretActionTriggered)
                if (HaveGoldForUpgradeTurrets)
                    UpgradeAllTurrets();
            
            if (BuildTurretActionTriggered)
                if(HaveGoldForBuildNewTurret)
                    if (NoCollisionInPlace())
                        BuildNewTurret();
        }

        private bool NoCollisionInPlace() =>
            !Physics2D.BoxCast(PointInWorld, Vector2.one * 1.2f, 0f, Vector2.zero);

        private void UpgradeAvatar() {
            GoldSystem.ModifyGold(-AvatarCost);
            _playerAvatar.UpgradeAvatar();
        }

        private void BuildNewTurret() {
            GoldSystem.ModifyGold(-BuildTurretCost);
            _autoTurretBuilder.Build(PointInWorld);
        }

        private void UpgradeAllTurrets() {
            GoldSystem.ModifyGold(-UpgradeTurretsCost);
            _autoTurretBuilder.UpgradeAllTurrets();
        }
    }
}
