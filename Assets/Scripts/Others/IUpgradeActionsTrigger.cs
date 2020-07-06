namespace Frogi {
    public interface IUpgradeActionsTrigger {
        bool UpgradeAvatarActionTriggered { get; }
        bool BuildTurretActionTriggered { get; }
        bool UpgradeTurretActionTriggered { get; }
    }
}
