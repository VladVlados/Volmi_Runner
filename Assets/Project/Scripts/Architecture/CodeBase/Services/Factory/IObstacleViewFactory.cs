using Project.Scripts.Architecture.CodeBase.Gameplay.Level;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public interface IObstacleViewFactory : IService {
    T Create<T>(ObstacleName name) where T : ObstacleView;
    T Create<T>(ObstacleType type) where T : ObstacleView;
  }
}