using System;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public class ObstacleViewPool : ObjectPool<ObstacleView> {
    public ObstacleViewPool(Func<ObstacleView> objectGenerator) : base(objectGenerator) { }
  }
}