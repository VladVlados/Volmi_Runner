using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay {
  public class RewardItemUI : FactoryPoolablePrefab {
    [SerializeField]
    private Image _itemView; 
    [SerializeField]
    private TMP_Text _textView;
    
    private RewardConfigurations _rewardConfigurations;
    
    [Inject]
    public void Construct(IGlobalDataService globalDataService) {
      _rewardConfigurations = globalDataService.GetGlobalData<RewardConfigurations>();
    }
    
    public void UpdateInfo(ObstacleName rewardObjectName, int count) {
      _textView.text = count.ToString();
      _itemView.sprite = _rewardConfigurations.GetRewardSprite(rewardObjectName);
    }

  }
}