using System;
using UnityEngine.UI;
using UnityEngine;

namespace Frogi {
    public class UserInterfaceController : MonoBehaviour {
        [SerializeField] private Text _goldText;
        [SerializeField] private Text _avatarCostText;
        [SerializeField] private Text _turretBuildCostText;
        [SerializeField] private Text _turretUpgradeCostText;
        [SerializeField] private Text _avatarHealth;
        [SerializeField] private Text _avatarDamagePerSecond;
        [SerializeField] private Text _turretDamagePerSecond;
        [SerializeField] private Text _difficultyLevel;
        [SerializeField] private Text _monstersHealthPoints;
        [SerializeField] private Text _monstersDamagePerSecond;
        [SerializeField] private GameObject _deathPanel;
        [SerializeField] private GameObject _winPanel;

        private void Start() {
            GoldSystem.OnGoldChange += ChangeDisplayedGold;
            AutoTurretBuilder.OnBuildOrUpgradeTurret += ChangeDisplayedTurretCost;
            PlayerAvatar.OnAvatarCostChange += ChangeAvatarCost;
            PlayerAvatar.OnAvatarUpgrade += ChangeAvatarDamagePerSecond;
            PlayerAvatar.OnAvatarHealthChange += ChangeAvatarHealth;
            AutoTurretBuilder.OnUpgradeTurret += ChangeTurretDamagePerSecond;
            MonsterSpawner.OnMonsterUpgrade += ChangeMonsterPanel;
            PlayerAvatar.OnDeath += ShowDeathPanel;
            GameManager.OnWin += ShowWinPanel;
            _deathPanel.SetActive(false);
            _winPanel.SetActive(false);
        }

        private void OnDisable() {
            GoldSystem.OnGoldChange -= ChangeDisplayedGold;
            AutoTurretBuilder.OnBuildOrUpgradeTurret -= ChangeDisplayedTurretCost;
            PlayerAvatar.OnAvatarCostChange -= ChangeAvatarCost;
            PlayerAvatar.OnAvatarUpgrade -= ChangeAvatarDamagePerSecond;
            PlayerAvatar.OnAvatarHealthChange -= ChangeAvatarHealth;
            AutoTurretBuilder.OnUpgradeTurret -= ChangeTurretDamagePerSecond;
            MonsterSpawner.OnMonsterUpgrade -= ChangeMonsterPanel;
            PlayerAvatar.OnDeath -= ShowDeathPanel;
            GameManager.OnWin -= ShowWinPanel;
        }

        private void ChangeDisplayedTurretCost(int buildCost, int upgradeCost) {
            _turretBuildCostText.text = $"(RMB) New turret: {buildCost}$";
            _turretUpgradeCostText.text = $"(U) Upgrade turrets: {upgradeCost}$";
        }

        private void ChangeAvatarCost(int avatarCost) => _avatarCostText.text = $"(Space) Avatar cost: {avatarCost}";

        private void ChangeDisplayedGold(int gold) => _goldText.text = $"Gold: {gold}$";

        private void ChangeAvatarHealth(int health, int maxHealth) =>
            _avatarHealth.text = $"Avatar health: {health}/{maxHealth}";

        private void ChangeAvatarDamagePerSecond(float dps) =>
            _avatarDamagePerSecond.text = $"Avatar DPS: {Mathf.Round(dps)}";

        private void ChangeTurretDamagePerSecond(float dps) =>
            _turretDamagePerSecond.text = $"Turret DPS: {Mathf.Round(dps)}";

        private void ChangeMonsterPanel(int difficulty, int monsterHealthPoint, float monsterDamagePerSecond) {
            _difficultyLevel.text = $"(;) Difficulty: {difficulty}";
            _monstersHealthPoints.text = $"Monster HP: {monsterHealthPoint}";
            _monstersDamagePerSecond.text = $"Monster DPS: {Mathf.Round(monsterDamagePerSecond)}";
        }

        private void ShowDeathPanel() => _deathPanel.SetActive(true);
        
        private void ShowWinPanel() => _winPanel.SetActive(true);
    }
}
