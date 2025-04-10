using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types {
  [CreateAssetMenu(fileName = "PlayerConfigurations", menuName = "GlobalData/PlayerConfigurations")]
  public class PlayerConfigurations : ItemGlobalData {
    [SerializeField]
    private float _stepMoveSpeed;

    public float StepMoveSpeed {
      get {
        return _stepMoveSpeed;
      }
    }
  }
}