namespace Upgrade
{
    public interface IUpgradable
    {
        public int CurrentLevel { get; }
        public void Upgrade();
        public UpgradeParametersBase GetUpgradeParameters();
    }
}