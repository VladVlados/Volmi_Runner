using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.Services.Events;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Level {
  public interface ILevelMoveSimulator {
    public void StartMove(ConfigurationInfo configurationInfo);

    public void StopMove();
  }

  public class LevelMoveSimulator : ILevelMoveSimulator {
    private readonly ILevelGenerator _levelGenerator;
    private readonly IMonoEventsService _monoEventsService;
    private readonly ILevelAreaSetup _levelAreaSetup;
    private readonly IObstacleProvider _obstacleProvider;

    private ConfigurationInfo _info;

    public LevelMoveSimulator(ILevelGenerator levelGenerator, IMonoEventsService monoEventsService, ILevelAreaSetup levelAreaSetup, IObstacleProvider obstacleProvider) {
      _levelGenerator = levelGenerator;
      _monoEventsService = monoEventsService;
      _levelAreaSetup = levelAreaSetup;
      _obstacleProvider = obstacleProvider;
    }

    public void StartMove(ConfigurationInfo configurationInfo) {
      _info = configurationInfo;
      _monoEventsService.OnUpdate += Move;
    }

    public void StopMove() {
      _monoEventsService.OnUpdate -= Move;
    }

    private void Move(object sender, EventArgs e) {
      foreach (TileView tile in Tiles) {
        tile.transform.Translate(Vector3.back * _info.Speed * Time.deltaTime);
      }

      TileView first = Tiles.Peek();
      TileView last = Tiles.Last();

      if (first.transform.position.z < _levelAreaSetup.StartPoint.position.z) {
        Vector3 tileLength = Vector3.forward * last.Collider.size.x;

        TileView movedTileView = Tiles.Dequeue();
        movedTileView.transform.position = last.transform.position + tileLength;
        Tiles.Enqueue(movedTileView);
        _obstacleProvider.SpawnRandomObstacle(movedTileView);
      }
    }

    private Queue<TileView> Tiles {
      get {
        return _levelGenerator.Tiles;
      }
    }
  }
}