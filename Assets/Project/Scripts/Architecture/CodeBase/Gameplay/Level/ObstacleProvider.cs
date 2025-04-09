using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Level {
  public interface IObstacleProvider {
    void SpawnRandomObstacle(TileView view);
  }

  public class ObstacleProvider : IObstacleProvider {
    private readonly List<ObstacleViewPool> _pools = new();

    public ObstacleProvider(IObstacleViewFactory gameFactory) {
      _pools.Add(new ObstacleViewPool(() => gameFactory.Create<ObstacleView>(ObstacleType.Reward)));
      _pools.Add(new ObstacleViewPool(() => gameFactory.Create<ObstacleView>(ObstacleType.Barrier)));
    }

    public void SpawnRandomObstacle(TileView view) {
      ObstacleView reward = _pools[Random.Range(0, _pools.Count)].Get();
      Transform randomPoint = view.TileSpawnPoints.GetRandomPint();
      
      reward.transform.position = randomPoint.position;
      reward.transform.parent = randomPoint;
      reward.SetupRandomRotation();
      
      view.RemoveObstacle();
      view.AddObstacle(reward);
    }
  }
}