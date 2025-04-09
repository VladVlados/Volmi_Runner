using System;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Meta {
  public interface IPlayerCollisionHandler {
    event Action<ObstacleName, ObstacleType> OnObstacleCollision;
    void ProcessCollision(IInteractable target);
  }

  public class PlayerCollisionHandler : IPlayerCollisionHandler {
    public event Action<ObstacleName, ObstacleType> OnObstacleCollision;

    public void ProcessCollision(IInteractable target) {
      if (target is ObstacleView view) {
        ProcessObstacleCollision(view);
      }

      target.OnInteractable();
    }

    private void ProcessObstacleCollision(ObstacleView view) {
      OnObstacleCollision?.Invoke(view.ObstacleInfo.ObstacleName, view.ObstacleInfo.Type);
    }
  }
}