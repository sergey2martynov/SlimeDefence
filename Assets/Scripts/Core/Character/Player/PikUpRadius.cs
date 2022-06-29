using Character.Player;
using Core.Character.Player;
using StaticData.Player;
using UnityEngine;
using Upgrade;

public class PikUpRadius : Upgradable
{
   [SerializeField] private PikUpRadiusLevels _pikUpRadiusLevels;
   [SerializeField] private CapsuleCollider _capsuleCollider;
   [SerializeField] private Player _player;
   public Player Controller => _player;

   private void Start()
   {
      SetRadius();
      MaxLevel = _pikUpRadiusLevels.GetMaxNumberOfLevel();
   }
   
   public override void Upgrade()
   {
      _currentLevel++;
      SetRadius();
   }

   private void SetRadius()
   {
      _capsuleCollider.radius = _pikUpRadiusLevels.GetRadiusParameters(_currentLevel).Amount;
   }
   
   public override UpgradeParametersBase GetUpgradeParameters()
   {
      return _pikUpRadiusLevels.GetRadiusParameters(_currentLevel + 1);
   }
}
