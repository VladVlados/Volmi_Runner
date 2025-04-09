using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Level {
  public interface ILevelAreaSetup {
    Transform TileHolder { get; }
    Transform StartPoint { get; }
  }

  public class LevelAreaSetup : MonoBehaviour, ILevelAreaSetup {
    [SerializeField]
    private Transform _tileHolder;
    [SerializeField]
    private Transform _startPoint;

    public Transform TileHolder {
      get {
        return _tileHolder;
      }
    }
    
    public Transform StartPoint {
      get {
        return _startPoint;
      }
    }
  }
}