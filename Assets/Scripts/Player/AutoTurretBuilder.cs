using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frogi {
    public class AutoTurretBuilder : MonoBehaviour {
        public static event Action<int, int> OnBuildOrUpgradeTurret = delegate { };
        public static event Action<float> OnUpgradeTurret = delegate {  };
        private static List<Shooter> _autoTurrets;
        
        private const float _SHOT_SPEED_UPGRADE = 0.1f;
        private const int _SHOT_DAMAGE_UPGRADE = 1;
        
        private int _baseBuildCost;
        private int _baseUpgradeCost;
        private int _turretLevel;

        public int BuildCost => _baseBuildCost * (_turretLevel + 1);
        public int UpgradeCost => _baseUpgradeCost * (_turretLevel + 1) * (_autoTurrets.Count + 1);

        private float DamagePerSecond {
            get {
                var dps = 0f;
                if (_autoTurrets.Count > 0) dps = _autoTurrets[0].DamagePerSecond;

                return dps;
            }
        }

        private void Awake() {
            _autoTurrets = new List<Shooter>();
            _baseBuildCost = 100;
            _baseUpgradeCost = 50;
            _turretLevel = 0;
        }

        public void Build(Vector3 position) {
            var autoTurret = AutoTurretPool.Instance.Get();
            SetUpNewTurret(autoTurret.gameObject, position);
            var autoLaser = autoTurret.GetComponent<Shooter>();
            UpgradeNewTurret(autoLaser);
            _autoTurrets.Add(autoLaser);
            UpdateCostsAndInfo();
        }

        public void UpgradeAllTurrets() {
            foreach (var autoTurret in _autoTurrets) {
                autoTurret.UpgradeShotsPerSecond(_SHOT_SPEED_UPGRADE);
                autoTurret.UpgradeShotsDamage(_SHOT_DAMAGE_UPGRADE);
            }
            
            _turretLevel++;
            UpdateCostsAndInfo();
        }
        
        private static void SetUpNewTurret(GameObject autoTurret, Vector3 position) {
            autoTurret.transform.position = position;
            autoTurret.SetActive(true);
        }

        private void UpgradeNewTurret(Shooter autoTurret) {
            autoTurret.UpgradeShotsPerSecond(_SHOT_SPEED_UPGRADE * _turretLevel);
            autoTurret.UpgradeShotsDamage(_SHOT_DAMAGE_UPGRADE * _turretLevel);
        }

        private void UpdateCostsAndInfo() {
            OnBuildOrUpgradeTurret(BuildCost, UpgradeCost);
            OnUpgradeTurret(DamagePerSecond);
        }
    }
}
    